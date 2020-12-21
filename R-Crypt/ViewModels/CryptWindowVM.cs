using R_Crypt.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            Vis_HomeGrid = Visibility.Visible;
            Vis_EncryptGrid = Visibility.Hidden;
            Vis_DecryptGrid = Visibility.Hidden;
            Vis_OptionsGrid = Visibility.Hidden;
        }

        private void CMD_GoTo_OptionsEvent(object obj)
        {

        }

        private void CMD_Goto_DecryptEvent(object obj)
        {

        }

        private void CMD_GoTo_EncryptEvent(object obj)
        {

        }

        private void CMD_GoTo_HomeEvent(object obj)
        {

        }

        private void CMD_HelpEvent(object obj)
        {

        }

        private void CMD_UserEvent(object obj)
        {

        }


        public Visibility Vis_HomeGrid { get => vis_HomeGrid; set => vis_HomeGrid = value; }
        private Visibility vis_HomeGrid;

        public Visibility Vis_EncryptGrid { get => vis_EncryptGrid; set => vis_EncryptGrid = value; }
        private Visibility vis_EncryptGrid;

        public Visibility Vis_DecryptGrid { get => vis_DecryptGrid; set => vis_DecryptGrid = value; }
        private Visibility vis_DecryptGrid;

        public Visibility Vis_OptionsGrid { get => vis_OptionsGrid; set => vis_OptionsGrid = value; }
        private Visibility vis_OptionsGrid;




    }
}
