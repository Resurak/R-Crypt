using R_Crypt.Common;
using R_Crypt.Common.Handlers;
using R_Crypt.Common.Utils;
using R_Crypt.Models;
using R_Crypt.Models.Serializable;
using R_Crypt.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace R_Crypt.ViewModels
{
    public class CryptWindowVM : BaseVM
    {
        public RelayCommand CMD_User { get; private set; }
        public RelayCommand CMD_Help { get; private set; }
        public RelayCommand CMD_GoTo_Home { get; private set; }
        public RelayCommand CMD_GoTo_Encrypt { get; private set; }
        public RelayCommand CMD_Goto_Decrypt { get; private set; }
        public RelayCommand CMD_GoTo_Options { get; private set; }
        public RelayCommand CMD_Process_StartEncryption { get; private set; }
        public RelayCommand CMD_Process_StartDecryption { get; private set; }

        public CryptWindowVM()
        {
            AddCommands();
            SetDefaults();
        }

        private void AddCommands()
        {
            CMD_User = new RelayCommand(CMD_UserEvent);
            CMD_Help = new RelayCommand(CMD_HelpEvent);
            CMD_GoTo_Home = new RelayCommand(CMD_GoTo_HomeEvent);
            CMD_GoTo_Encrypt = new RelayCommand(CMD_GoTo_EncryptEvent);
            CMD_Goto_Decrypt = new RelayCommand(CMD_Goto_DecryptEvent);
            CMD_GoTo_Options = new RelayCommand(CMD_GoTo_OptionsEvent);

            CMD_Process_StartEncryption = new RelayCommand(CMD_StartEncryptionEvent);
            CMD_Process_StartDecryption = new RelayCommand(CMD_StartDecryptionEvent);
        }

        private void SetDefaults()
        {
            Vis_HomeGrid = Visibility.Visible;
            Vis_EncryptGrid = Visibility.Hidden;
            Vis_DecryptGrid = Visibility.Hidden;
            Vis_OptionsGrid = Visibility.Hidden;
        }

        private void CMD_GoTo_OptionsEvent(object obj)
        {
            //Vis_HomeGrid = Visibility.Hidden;
            Vis_EncryptGrid = Visibility.Hidden;
            Vis_DecryptGrid = Visibility.Hidden;
            Vis_OptionsGrid = Visibility.Visible;

            Config.MainConfig.Program_Bool_AutoEncryptActive = true;
            Config.MainConfig.Stats_Int_FilesEncrypted = 27;
            Config.MainConfig.Stats_Long_BiggestDecrypted = 18435464;
        }

        private void CMD_Goto_DecryptEvent(object obj)
        {
            Vis_HomeGrid = Visibility.Hidden;
            Vis_EncryptGrid = Visibility.Hidden;
            Vis_DecryptGrid = Visibility.Visible;
            Vis_OptionsGrid = Visibility.Hidden;
        }

        private void CMD_GoTo_EncryptEvent(object obj)
        {
            Vis_HomeGrid = Visibility.Hidden;
            Vis_EncryptGrid = Visibility.Visible;
            Vis_DecryptGrid = Visibility.Hidden;
            Vis_OptionsGrid = Visibility.Hidden;
        }

        private void CMD_GoTo_HomeEvent(object obj)
        {
            SetDefaults();
        }

        private void CMD_HelpEvent(object obj)
        {

        }

        private void CMD_UserEvent(object obj)
        {

        }

        private void CMD_StartEncryptionEvent(object obj)
        {

        }

        private void CMD_StartDecryptionEvent(object obj)
        {

        }


        public Visibility Vis_HomeGrid { get => vis_HomeGrid; set { vis_HomeGrid = value; Notify(); } }
        private Visibility vis_HomeGrid = Visibility.Hidden;

        public Visibility Vis_EncryptGrid { get => vis_EncryptGrid; set { vis_EncryptGrid = value; Notify(); } }
        private Visibility vis_EncryptGrid = Visibility.Hidden;

        public Visibility Vis_DecryptGrid { get => vis_DecryptGrid; set { vis_DecryptGrid = value; Notify(); } }
        private Visibility vis_DecryptGrid = Visibility.Hidden;

        public Visibility Vis_OptionsGrid { get => vis_OptionsGrid; set { vis_OptionsGrid = value; Notify(); } }
        private Visibility vis_OptionsGrid = Visibility.Hidden;


        public ConfigHandler Config { get => _Config; set { _Config = value; Notify(); } }
        private ConfigHandler _Config = new();

        public CryptoFilesHandler FilesHandler { get => _FilesHandler; set { _FilesHandler = value; Notify(); } }
        private CryptoFilesHandler _FilesHandler = new();
        
    }
}
