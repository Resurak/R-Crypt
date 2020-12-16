using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using R_Crypt.Models;
using R_Crypt.Views;

namespace R_Crypt.Common
{
    public class StartupHandler : ProgramBase
    {
        public StartupHandler()
        {

        }

        public static async void LoadConfig()
        {
            ProgramBase programBase = new();
            StartupType Startup = new();

            if (!Directory.Exists(programBase.ConfigFolder_Path)) Directory.CreateDirectory(programBase.ConfigFolder_Path);

            await Task.Run(() =>
            {
                if (File.Exists(programBase.ConfigFile_Path))
                {
                    ProgramWideConfig = ConfigHandler.DeserializeConfig(programBase.ConfigFile_Path);
                    Startup = StartupType.NoUser;
                }
                else
                {
                    ConfigHandler.SerializeConfig(ProgramWideConfig, programBase.ConfigFile_Path);
                    Startup = StartupType.FirstTime;
                }
            });

            if (Startup == StartupType.FirstTime)
            {

            }
            else if (Startup == StartupType.NoUser)
            {
                MainWindow mw = new();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = mw;
                mw.Show();
            }
            else if (Startup == StartupType.WithUser)
            {
                LoginWindow lw = new();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = lw;
                lw.Show();
            }
        }

        enum StartupType { FirstTime, NoUser, WithUser}
    }
}
