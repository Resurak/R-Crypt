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
using R_Crypt.Models.Base;
using System.Security.Permissions;

namespace R_Crypt.Common.Handlers
{
    public class StartupHandler
    {
        public static StartupType Startup = new();
        public static bool Loaded = false;

        public static BaseProgram BaseProgram = new();
        public static Config Config = new();

        public StartupHandler()
        {

        }

        public static async void LoadConfig()
        {
            if (!Directory.Exists(BaseProgram.Base_Path_ConfigFolder)) Directory.CreateDirectory(BaseProgram.Base_Path_ConfigFolder);

            ConfigHandler.SerializeConfig(Config, BaseProgram.Base_Path_ConfigFile);

            await Task.Run(() =>
            {
                if (File.Exists(BaseProgram.Base_Path_ConfigFile))
                {
                    Config = ConfigHandler.DeserializeConfig(BaseProgram.Base_Path_ConfigFile);

                    if (Config.Opt_Bool_NoUser) Startup = StartupType.NoUser;
                    else Startup = StartupType.NoUser;
                }
                else
                {
                    ConfigHandler.SerializeConfig(Config, BaseProgram.Base_Path_ConfigFile);
                    Startup = StartupType.NoUser; // se il file non esiste allora dà errore. da implementare dopo aver fatto l'installer
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

                if (File.Exists(BaseProgram.Base_Path_ExeFile))
                {
                    Config.Program_Str_CurrentSHA256 = Utils.CommonUtilities.CheckFileSHA256(Config.Program_Str_CurrentExePath);

                    if (Config.Program_Str_LastFileSHA256 != Config.Program_Str_CurrentSHA256)
                        Utils.CommonUtilities.CopyExeToPath(BaseProgram.Base_Path_ExeFile);
                }
                else Utils.CommonUtilities.CopyExeToPath(BaseProgram.Base_Path_ExeFile);

                Config.Program_Str_LastFileSHA256 = Config.Program_Str_CurrentSHA256;

                //Thread.Sleep(3000);

                ConfigHandler.SerializeConfig(Config, BaseProgram.Base_Path_ConfigFile);
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
        }

        public enum StartupType { FirstTime, NoUser, WithUser}
    }
}
