using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using R_Crypt.Models;
using R_Crypt.Views;
using R_Crypt.Models.Serializable;
using R_Crypt.Crypt;

namespace R_Crypt.Common
{
    public class StartupHandler : ProgramBase
    {
        public static ProgramBase ProgramBase = new();
        public static StartupType Startup = new();
        public static bool Loaded = false;

        public StartupHandler()
        {

        }

        public static async void LoadConfig()
        {
            //CryptHandler crypt = new();
            //crypt.EncryptConfig("prova");

            if (!Directory.Exists(ProgramBase.ConfigFolder_Path)) Directory.CreateDirectory(ProgramBase.ConfigFolder_Path);

            ConfigHandler.SerializeConfig(ProgramWideConfig, ProgramBase.ConfigFile_Path);

            await Task.Run(() =>
            {
                if (File.Exists(ProgramBase.ConfigFile_Path))
                {
                    ProgramWideConfig = ConfigHandler.DeserializeConfig(ProgramBase.ConfigFile_Path);

                    if (ProgramWideConfig.NoUser) Startup = StartupType.NoUser;
                    else Startup = StartupType.NoUser;
                }
                else
                {
                    ConfigHandler.SerializeConfig(ProgramWideConfig, ProgramBase.ConfigFile_Path);
                    Startup = StartupType.FirstTime;
                }

                if (string.IsNullOrWhiteSpace(ProgramWideConfig.ExePath))
                {
                    ProgramWideConfig.ExePath = ProgramBase.ExeFile_Path;
                }

                Loaded = true;
            });
        }

        public static async void StartProgram()
        {
            await Task.Run(() =>
            {
                while (Loaded == false)
                {
                    Thread.Sleep(100);
                }

                if (File.Exists(ProgramWideConfig.ExePath))
                {
                    ProgramWideConfig.CurrentExeSha256 = Utils.CommonUtilities.CheckFileSHA256(ProgramBase.CurrentExePath);

                    if (ProgramWideConfig.LastExeSha256 != ProgramWideConfig.CurrentExeSha256)
                        Utils.CommonUtilities.CopyExeToPath(ProgramWideConfig.ExePath);
                }
                else Utils.CommonUtilities.CopyExeToPath(ProgramWideConfig.ExePath);

                ProgramWideConfig.LastExeSha256 = ProgramWideConfig.CurrentExeSha256;

                //Thread.Sleep(3000);

                ConfigHandler.SaveChanges();
            });

            if (Startup == StartupType.FirstTime)
            {
                FirstLoadWindow flw = new();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = flw;
                flw.Show();
            }
            else if (Startup == StartupType.NoUser)
            {
                //MainWindow mw = new();
                CryptWindow mw = new();
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

            //CryptHandler crypt = new();
            //crypt.EncryptConfig("prova");
        }

        public enum StartupType { FirstTime, NoUser, WithUser}
    }
}
