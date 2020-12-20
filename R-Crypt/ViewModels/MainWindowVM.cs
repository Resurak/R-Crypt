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
                    CryptoFiles.Add(new CryptoFile(file));
            }
        }

        public ObservableCollection<CryptoFile> CryptoFiles { get => _CryptoFiles; set { _CryptoFiles = value; Notify(); } }
        private ObservableCollection<CryptoFile> _CryptoFiles = new();

        public void AddFilesToList(string[] files)
        {
            foreach (var file in files)
            {
                CryptoFiles.Add(new CryptoFile(file));
            }

            ObservableCollection<CryptoFile> orderedByFolder =
                new ObservableCollection<CryptoFile>(CryptoFiles.Where(folder => folder.IsFolder == true).OrderBy(folder => folder.PathText).ToList());

            ObservableCollection<CryptoFile> orderedByType =
                new ObservableCollection<CryptoFile>(CryptoFiles.Where(file => file.IsFolder == false).OrderBy(file => file.PathText).OrderBy(file => file.FileTypeText).ToList());

            CryptoFiles = 
                new ObservableCollection<CryptoFile>(orderedByFolder.Concat(orderedByType).ToList());
        } 
    }
}
