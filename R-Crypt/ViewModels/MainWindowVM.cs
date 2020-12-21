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
using R_Crypt.Common;

namespace R_Crypt.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        public RelayCommand CMD_Encrypt { get; private set; }
        public RelayCommand CMD_Decrypt { get; private set; }
        public RelayCommand CMD_Options { get; private set; }
        public RelayCommand CMD_AddFiles { get; private set; }
        public RelayCommand CMD_StartCrypt { get; private set; }

        public MainWindowVM()
        {
            CryptoFiles = new();

            SelectedMode = CryptMode.Encrypt;

            CMD_AddFiles = new RelayCommand(param => CMD_AddFilesEvent(param));
            CMD_Encrypt = new RelayCommand(param => CMD_EncryptEvent(param));
            CMD_Decrypt = new RelayCommand(param => CMD_DecryptEvent(param));
            CMD_Options = new RelayCommand(param => CMD_OptionsEvent(param));
            CMD_StartCrypt = new RelayCommand(param => CMD_StartEvent(param));
        }

        private void CMD_StartEvent(object param)
        {
            if (SelectedMode == CryptMode.Encrypt)
            {
                if (CryptoFiles.Count > 0)
                {
                    int index = 0;
                    foreach (var file in CryptoFiles)
                    {
                        if (!file.Bool_IsFolder)
                        {
                            BackgroundWorker worker = new();
                            worker.DoWork += (sender, e) =>
                            {
                                file.Vis_ProgressBar = Visibility.Visible;
                                file.Vis_TextBlock = Visibility.Hidden;

                                CryptHandler cryptHandler = new();
                                cryptHandler.CryptProgressChanged += (sender, e) =>
                                {
                                    file.Long_ProcessedSize = e.CurrentByte;
                                    UpdateUI();
                                };
                                cryptHandler.CryptCompleted += (sender, e) =>
                                {
                                    file.Str_ProgressTracker = "Encrypted";
                                    file.Vis_ProgressBar = Visibility.Hidden;
                                    file.Vis_TextBlock = Visibility.Visible;
                                };
                                cryptHandler.EncryptFileAsync("prova", file.Str_Path);
                            };
                            worker.RunWorkerAsync();
                            index++;
                        }
                    }
                }
            }
        }

        private void CMD_OptionsEvent(object param)
        {

        }

        private void CMD_DecryptEvent(object param)
        {
            SelectedMode = CryptMode.Decrypt;
        }

        private void CMD_EncryptEvent(object param)
        {
            SelectedMode = CryptMode.Encrypt;
        }

        private void CMD_AddFilesEvent(object param)
        {
            OpenFileDialog fileDialog = new();
            fileDialog.CheckFileExists = true;
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fileDialog.Multiselect = true;

            if (fileDialog.ShowDialog() == true)
            {
                foreach (var file in fileDialog.FileNames)
                    CryptoFiles.Add(file.GetCryptoFile());
            }
        }

        public Visibility Vis_DragAndDropGrid { get => _Vis_DragAndDropGrid; set { _Vis_DragAndDropGrid = value; Notify(); } }
        private Visibility _Vis_DragAndDropGrid = Visibility.Visible;


        public CryptMode SelectedMode { get => _SelectedMode; set { _SelectedMode = value; RefreshSelectMode(); } }
        private CryptMode _SelectedMode = new();

        public string Str_SelectedModeText { get => _Str_SelectedModeText; set { _Str_SelectedModeText = value; Notify(); } }
        private string _Str_SelectedModeText = "";


        public ObservableCollection<CryptoFile> CryptoFiles { get => _CryptoFiles; set { _CryptoFiles = value; Notify(); } }
        private ObservableCollection<CryptoFile> _CryptoFiles = new AsyncObservableCollection<CryptoFile>();


        public string Str_TotalSize { get => _Str_TotalSize; set { _Str_TotalSize = value; Notify(); } }
        private string _Str_TotalSize = "";

        public string Str_TotalProcessedSize { get => _Str_TotalProcessedSize; set { _Str_TotalProcessedSize = value; Notify(); } }
        private string _Str_TotalProcessedSize = "";

        public string Str_CurrentProcessedSize { get => _Str_CurrentProcessedSize; set { _Str_CurrentProcessedSize = value; Notify(); } }
        private string _Str_CurrentProcessedSize = "";


        public long Long_TotalSize { get => _Long_TotalSize; set { _Long_TotalSize = value; Notify(); } }
        private long _Long_TotalSize = 0;

        public long Long_CurrentProcessedSize { get => _Long_CurrentProcessedSize; set { _Long_CurrentProcessedSize = value; Notify(); } }
        private long _Long_CurrentProcessedSize = -1;


        public SolidColorBrush Color_SizeFeedback { get => _Color_SizeFeedback; set { _Color_SizeFeedback = value; Notify(); } }
        private SolidColorBrush _Color_SizeFeedback = new SolidColorBrush(Colors.Lime);

        SolidColorBrush yellow = new SolidColorBrush(Colors.Yellow);
        SolidColorBrush red = new SolidColorBrush(Colors.Red);


        public void AddFilesToList(string[] files)
        {
            foreach (var file in files)
            {
                CryptoFiles.Add(file.GetCryptoFile());
            }

            ObservableCollection<CryptoFile> orderedByFolder =
                new ObservableCollection<CryptoFile>(CryptoFiles.Where(folder => folder.Bool_IsFolder == true).OrderBy(folder => folder.Str_FileName).ToList());

            ObservableCollection<CryptoFile> orderedByType =
                new ObservableCollection<CryptoFile>(CryptoFiles.Where(file => file.Bool_IsFolder == false).OrderBy(file => file.Str_FileName).OrderBy(file => file.Str_FileType).ToList());

            CryptoFiles =
                new ObservableCollection<CryptoFile>(orderedByFolder.Concat(orderedByType).ToList());

            UpdateUI();
        }

        public void RemoveFileFromList(List<CryptoFile> files)
        {
            foreach (var file in files)
                CryptoFiles.Remove(file);

            UpdateUI();
        }

        private void RefreshSelectMode()
        {
            if (SelectedMode == CryptMode.Encrypt)
            {
                _Str_SelectedModeText = "Encrypt";
            }
            else if (SelectedMode == CryptMode.Decrypt)
            {
                _Str_SelectedModeText = "Decrypt";
            }
        }

        private void UpdateUI()
        {
            if (CryptoFiles.Count > 0)
                Vis_DragAndDropGrid = Visibility.Collapsed;

            long tempSize = 0;
            long currentSize = 0;

            if (CryptoFiles.Count > 0)
            {
                foreach (var file in CryptoFiles)
                {
                    tempSize += file.Long_FileSize;
                    currentSize += file.Long_ProcessedSize;
                }

                Str_CurrentProcessedSize = currentSize.ConvertLongByteToString();
                Long_CurrentProcessedSize = currentSize;

                Str_TotalSize = tempSize.ConvertLongByteToString();
                Long_TotalSize = tempSize;

                if (tempSize >= 104857600 && tempSize < 1073741824)
                    Color_SizeFeedback = yellow;

                else if (tempSize >= 1073741824)
                    Color_SizeFeedback = red;
            }
            else
            {
                Str_CurrentProcessedSize = tempSize.ConvertLongByteToString();
                Long_CurrentProcessedSize = tempSize;

                Str_TotalSize = tempSize.ConvertLongByteToString();
                Long_TotalSize = tempSize;
            }

            Str_TotalProcessedSize = $"{Str_CurrentProcessedSize} / {Str_TotalSize}"; 
        }

        public enum CryptMode { Encrypt, Decrypt }
    }
}
