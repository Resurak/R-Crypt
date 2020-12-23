﻿using R_Crypt.Common.Utils;
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
        }

        private void SetDefaults()
        {
            Vis_HomeGrid = Visibility.Hidden;
            Vis_EncryptGrid = Visibility.Visible;
            Vis_DecryptGrid = Visibility.Hidden;
            Vis_OptionsGrid = Visibility.Hidden;
        }

        private void CMD_GoTo_OptionsEvent(object obj)
        {
            Vis_HomeGrid = Visibility.Hidden;
            Vis_EncryptGrid = Visibility.Hidden;
            Vis_DecryptGrid = Visibility.Hidden;
            Vis_OptionsGrid = Visibility.Visible;

            Config.Program_Bool_AutoEncryptActive = true;
            Config.Stats_Int_FilesEncrypted = 27;
            Config.Stats_Long_BiggestDecrypted = 18435464;
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


        public Visibility Vis_HomeGrid { get => vis_HomeGrid; set { vis_HomeGrid = value; Notify(); } }
        private Visibility vis_HomeGrid;

        public Visibility Vis_EncryptGrid { get => vis_EncryptGrid; set { vis_EncryptGrid = value; Notify(); } }
        private Visibility vis_EncryptGrid;

        public Visibility Vis_DecryptGrid { get => vis_DecryptGrid; set { vis_DecryptGrid = value; Notify(); } }
        private Visibility vis_DecryptGrid;

        public Visibility Vis_OptionsGrid { get => vis_OptionsGrid; set { vis_OptionsGrid = value; Notify(); } }
        private Visibility vis_OptionsGrid;


        public Config Config { get => config; set { config = value; Notify(); } }
        private Config config = new();


        public SolidColorBrush Color_AutoEncryptActive { get => _Color_AutoEncryptActive; set { _Color_AutoEncryptActive = value; Notify(); } }
        private SolidColorBrush _Color_AutoEncryptActive = new SolidColorBrush(Colors.Lime);

        public string ProgramVersion { get => Config.Program_Str_Version; }

        public ObservableCollection<CryptoFile> CryptoFile_Config_EncryptedFiles { get => new ObservableCollection<CryptoFile>(Config.CryptoFiles_CurrentEncryptedFiles); }
        
        public ObservableCollection<CryptoFile> CryptoFile_Current_EncryptedFiles { get => CryptoFile_Current_EncryptedFiles; set { _CryptoFile_Current_EncryptedFiles = value; Notify(); } }
        private ObservableCollection<CryptoFile> _CryptoFile_Current_EncryptedFiles = new();
    }
}
