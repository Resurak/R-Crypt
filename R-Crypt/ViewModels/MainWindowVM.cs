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
using System.Windows.Media;

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

            SelectedMode = CryptMode.Encrypt;

            AddFilesCommand = new RelayCommand(param => AddFilesEvent(param));
            EncryptCommand = new RelayCommand(param => EncryptCommandEvent(param));
            DecryptCommand = new RelayCommand(param => DecryptCommandEvent(param));
            OptionsCommand = new RelayCommand(param => OptionsCommandEvent(param));
        }

        private void OptionsCommandEvent(object param)
        {

        }

        private void DecryptCommandEvent(object param)
        {
            SelectedMode = CryptMode.Decrypt;
        }

        private void EncryptCommandEvent(object param)
        {
            SelectedMode = CryptMode.Encrypt;
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

        public Visibility DragAndDropGridVisibility { get => _DragAndDropGridVisibility; set { _DragAndDropGridVisibility = value; Notify(); } }
        private Visibility _DragAndDropGridVisibility = Visibility.Visible;


        public CryptMode SelectedMode { get => _SelectedMode; set { _SelectedMode = value; RefreshSelectMode(); } }
        private CryptMode _SelectedMode = new();

        public string SelectedModeText { get => _SelectedModeText; set { _SelectedModeText = value; Notify(); } }
        private string _SelectedModeText = "";


        public ObservableCollection<CryptoFile> CryptoFiles { get => _CryptoFiles; set { _CryptoFiles = value; Notify(); } }
        private ObservableCollection<CryptoFile> _CryptoFiles = new();


        public string TotalFilesSize { get => _TotalFilesSize; set { _TotalFilesSize = value; Notify(); } }
        private string _TotalFilesSize = "";

        public string TotalProcessedBytesString { get => _TotalProcessedBytesString; set { _TotalProcessedBytesString = value; Notify(); } }
        private string _TotalProcessedBytesString = "-- B / -- B";

        public string CurrentProcessedBytesString { get => _CurrentProcessedBytesString; set { _CurrentProcessedBytesString = value; Notify(); } }
        private string _CurrentProcessedBytesString = "";



        public SolidColorBrush TooLargeFeedbackColor { get => _TooLargeFeedbackColor; set { _TooLargeFeedbackColor = value; Notify(); } }
        private SolidColorBrush _TooLargeFeedbackColor = new SolidColorBrush(Colors.Lime);

        SolidColorBrush yellow = new SolidColorBrush(Colors.Yellow);
        SolidColorBrush red = new SolidColorBrush(Colors.Red);



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

            long tempSize = 0;
            CurrentProcessedBytesString = tempSize.ConvertLongByteToString();

            foreach (var file in CryptoFiles) 
                tempSize += file.FileSize;

            TotalFilesSize = tempSize.ConvertLongByteToString();

            if (tempSize >= 104857600 && tempSize < 1073741824)
                TooLargeFeedbackColor = yellow;

            else if (tempSize >= 1073741824)
                TooLargeFeedbackColor = red;

            TotalProcessedBytesString = $"{CurrentProcessedBytesString} / {TotalFilesSize}";

            CheckIfListHasFile();
        } 

        public void RemoveFileFromList(List<CryptoFile> files)
        {
            foreach (var file in files)
                CryptoFiles.Remove(file);
        }

        private void CheckIfListHasFile()
        {
            if (CryptoFiles.Count > 0) 
                DragAndDropGridVisibility = Visibility.Collapsed;
        }

        private void RefreshSelectMode()
        {
            if (SelectedMode == CryptMode.Encrypt)
            {
                SelectedModeText = "Encrypt";
            }
            else if (SelectedMode == CryptMode.Decrypt)
            {
                SelectedModeText = "Decrypt";
            }
        }

        public enum CryptMode { Encrypt, Decrypt }
    }
}
