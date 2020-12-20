using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_Crypt.Common.Utils;
using R_Crypt.ViewModels.Base;
using R_Crypt.Views;
using R_Crypt.Models;
using R_Crypt.Models.Serializable;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;

namespace R_Crypt.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        public RelayCommand EncryptCommand { get; private set; }
        public RelayCommand DecryptCommand { get; private set; }
        public RelayCommand OptionsCommand { get; private set; }
        public RelayCommand AddFilesCommand { get; private set; }

        public MainWindowVM()
        {
            CryptoFiles = new();
            AddFilesCommand = new RelayCommand(param => AddFilesEvent(param));
            EncryptCommand = new RelayCommand(param => EncryptCommandEvent(param));
            DecryptCommand = new RelayCommand(param => DecryptCommandEvent(param));
            OptionsCommand = new RelayCommand(param => OptionsCommandEvent(param));
        }

        private void OptionsCommandEvent(object param)
        {
            throw new NotImplementedException();
        }

        private void DecryptCommandEvent(object param)
        {
            throw new NotImplementedException();
        }

        private void EncryptCommandEvent(object param)
        {
            throw new NotImplementedException();
        }

        private void AddFilesEvent(object param)
        {
            OpenFileDialog fileDialog = new();
            fileDialog.CheckFileExists = true;
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fileDialog.Multiselect = true;

            if (fileDialog.ShowDialog() == true)
            {
                foreach (var file in fileDialog.FileNames) 
                    CryptoFiles.Add(new CryptoFiles(file));
            }
        }

        public ObservableCollection<CryptoFiles> CryptoFiles { get; set; }

        public void AddFilesToList(string[] files)
        {
            foreach (var file in files)
            {
                CryptoFiles.Add(new CryptoFiles(file));
            }
        } 
    }
}
