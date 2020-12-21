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
using R_Crypt.Crypt;
using System.ComponentModel;
using System.Windows.Data;

namespace R_Crypt.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        public RelayCommand EncryptCommand { get; private set; }
        public RelayCommand DecryptCommand { get; private set; }
        public RelayCommand OptionsCommand { get; private set; }
        public RelayCommand AddFilesCommand { get; private set; }
        public RelayCommand StartCryptProcessCommand { get; private set; }

        public MainWindowVM()
        {
            CryptoFiles = new();

            SelectedMode = CryptMode.Encrypt;

            AddFilesCommand = new RelayCommand(param => AddFilesEvent(param));
            EncryptCommand = new RelayCommand(param => EncryptCommandEvent(param));
            DecryptCommand = new RelayCommand(param => DecryptCommandEvent(param));
            OptionsCommand = new RelayCommand(param => OptionsCommandEvent(param));
            StartCryptProcessCommand = new RelayCommand(param => StartCryptProcessCommandEvent(param));

            //BindingOperations.EnableCollectionSynchronization(CryptoFiles, _collectionLock);
        }

        private void OnProgressChanged()
        {
            //long totalProcessed = 0;
            //foreach (var file in CryptoFiles)
            //    totalProcessed += file.ProcessedBytes;

            UpdateUI();

            TotalProcessedBytesString = $"{CurrentProcessedBytesString} / {TotalFilesSize}";
        }

        private void StartCryptProcessCommandEvent(object param)
        {
            if (SelectedMode == CryptMode.Encrypt)
            {
                if (CryptoFiles.Count > 0)
                {
                    int index = 0;
                    foreach (var file in CryptoFiles)
                    {
                        if (!file.IsFolder)
                        {
                            BackgroundWorker worker = new();
                            worker.DoWork += (sender, e) =>
                            {
                                file.ProgressBarVis = Visibility.Visible;
                                file.TextProgressVis = Visibility.Hidden;

                                CryptHandler cryptHandler = new();
                                cryptHandler.CryptProgressChanged += (sender, e) =>
                                {
                                    file.ProcessedBytes = e.CurrentByte;
                                    OnProgressChanged();
                                };
                                cryptHandler.CryptCompleted += (sender, e) =>
                                {
                                    file.ProgressText = "Encrypted";
                                    file.ProgressBarVis = Visibility.Hidden;
                                    file.TextProgressVis = Visibility.Visible;
                                };
                                cryptHandler.EncryptFileAsync("prova", file.Path);
                            };
                            worker.RunWorkerAsync();
                            index++;
                        }
                    }
                }
            }
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

        private object _collectionLock = new object();

        public Visibility DragAndDropGridVisibility { get => _DragAndDropGridVisibility; set { _DragAndDropGridVisibility = value; Notify(); } }
        private Visibility _DragAndDropGridVisibility = Visibility.Visible;


        public CryptMode SelectedMode { get => _SelectedMode; set { _SelectedMode = value; RefreshSelectMode(); } }
        private CryptMode _SelectedMode = new();

        public string SelectedModeText { get => _SelectedModeText; set { _SelectedModeText = value; Notify(); } }
        private string _SelectedModeText = "";


        public ObservableCollection<CryptoFile> CryptoFiles { get => _CryptoFiles; set { _CryptoFiles = value; Notify(); } }
        private ObservableCollection<CryptoFile> _CryptoFiles = new AsyncObservableCollection<CryptoFile>();


        public string TotalFilesSize { get => _TotalFilesSize; set { _TotalFilesSize = value; Notify(); } }
        private string _TotalFilesSize = "";

        public string TotalProcessedBytesString { get => _TotalProcessedBytesString; set { _TotalProcessedBytesString = value; Notify(); } }
        private string _TotalProcessedBytesString = "";

        public string CurrentProcessedBytesString { get => _CurrentProcessedBytesString; set { _CurrentProcessedBytesString = value; Notify(); } }
        private string _CurrentProcessedBytesString = "";


        public long Long_TotalFilesSize { get => long_TotalFilesSize; set { long_TotalFilesSize = value; Notify(); } }
        private long long_TotalFilesSize = 0;

        public long Long_CurrentFilesSize { get => long_CurrentFilesSize; set { long_CurrentFilesSize = value; Notify(); } }
        private long long_CurrentFilesSize = -1;


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

            //long tempSize = 0;

            //CurrentProcessedBytesString = tempSize.ConvertLongByteToString();
            //Long_CurrentFilesSize = tempSize;

            //foreach (var file in CryptoFiles)
            //    tempSize += file.FileSize;

            //TotalFilesSize = tempSize.ConvertLongByteToString();
            //Long_TotalFilesSize = tempSize;

            //if (tempSize >= 104857600 && tempSize < 1073741824)
            //    TooLargeFeedbackColor = yellow;

            //else if (tempSize >= 1073741824)
            //    TooLargeFeedbackColor = red;

            UpdateUI();

            TotalProcessedBytesString = $"{CurrentProcessedBytesString} / {TotalFilesSize}";

            CheckIfListHasFile();
        }

        public void RemoveFileFromList(List<CryptoFile> files)
        {
            foreach (var file in files)
                CryptoFiles.Remove(file);

            UpdateUI();

            //long zero = 0;

            //TotalFilesSize = zero.ConvertLongByteToString();
            //TotalProcessedBytesString = zero.ConvertLongByteToString();
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

        private void UpdateUI()
        {
            long tempSize = 0;
            long currentSize = 0;

            if (CryptoFiles.Count > 0)
            {
                foreach (var file in CryptoFiles)
                {
                    tempSize += file.FileSize;
                    currentSize += file.ProcessedBytes;
                }

                CurrentProcessedBytesString = currentSize.ConvertLongByteToString();
                Long_CurrentFilesSize = currentSize;

                TotalFilesSize = tempSize.ConvertLongByteToString();
                Long_TotalFilesSize = tempSize;

                if (tempSize >= 104857600 && tempSize < 1073741824)
                    TooLargeFeedbackColor = yellow;

                else if (tempSize >= 1073741824)
                    TooLargeFeedbackColor = red;
            }
            else
            {
                CurrentProcessedBytesString = tempSize.ConvertLongByteToString();
                Long_CurrentFilesSize = tempSize;

                TotalFilesSize = tempSize.ConvertLongByteToString();
                Long_TotalFilesSize = tempSize;
            }
        }

        public enum CryptMode { Encrypt, Decrypt }
    }
}
