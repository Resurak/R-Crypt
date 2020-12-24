using R_Crypt.Common.Utils;
using R_Crypt.Models.Base;
using R_Crypt.Models.Serializable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace R_Crypt.Common.Handlers
{
    public class CryptoFilesHandler : BaseModel
    {
        private bool IsUpdatingCollection = false;

        public async void GetCryptoFiles(string[] files, bool overwrite)
        {
            if (overwrite && CryptoFiles.Count > 0) CryptoFiles.Clear();

            IsUpdatingCollection = true;

            if (files != null)
            {
                foreach (var file in files)
                {
                    CryptoFile crypto = new();

                    if (!string.IsNullOrEmpty(file))
                    {
                        if (file.CheckExist())
                        {
                            crypto.Bool_IsFolder = file.IsDirectoryOrFile();

                            crypto.Str_Path = file;
                            crypto.Str_FileName = crypto.Str_Path.GetFileName();

                            crypto.Tooltip = $"Path of the file: {crypto.Str_Path}";

                            if (crypto.Bool_IsFolder)
                            {
                                crypto.Icon = new BitmapImage(new Uri(@"pack://application:,,,/"
                                    + Assembly.GetExecutingAssembly().GetName().Name
                                    + ";component/"
                                    + "Resources/folder.png", UriKind.Absolute));

                                crypto.Long_FileSize = 0;
                                crypto.Str_FileSize = "";
                                crypto.Str_FileType = "Folder";
                            }
                            else
                            {
                                FileInfo info = new(file);

                                crypto.Long_FileSize = info.Length;

                                crypto.Str_FileSize = info.Length.ConvertLongByteToString();
                                crypto.Str_FileExtension = file.GetFileExtension();
                                crypto.Str_FileType = file.GetFileExtension().GetExtensionType();
                            }

                            crypto.Long_ProcessedSize = 0;
                            crypto.Str_ProgressTracker = "Waiting input";

                            crypto.Vis_ProgressBar = Visibility.Hidden;
                            crypto.Vis_TextBlock = Visibility.Visible;
                        }
                        else
                        {
                            crypto.Str_FileName = "Error";
                            crypto.Tooltip = $"Error. Cannot get {crypto.Str_Path}. Probably need admin privileges";
                            crypto.Error = true;
                        }

                        CryptoFiles.Add(crypto);
                    }
                    else CryptoFiles.Add(new CryptoFile { Str_FileName = "Error", Error = true });
                }
            }
            else throw new Exception("Files are null");

            OrderCollectionByFolder();
            OnCollectionChanged(overwrite);
        }

        private async void OnCollectionChanged(bool overwrite)
        {
            Int_TotalFiles = CryptoFiles.Count;

            Long_TotalSize = 0;
            Long_ProcessedSize = 0;

            int folders = 0;

            foreach (var file in CryptoFiles)
            {
                if (!file.Error)
                {
                    Long_TotalSize += file.Long_FileSize;
                    Long_ProcessedSize += file.Long_ProcessedSize;

                    if (file.Bool_IsFolder) folders++;
                }
            }

            if (folders > 0) Int_TotalFolders = folders;

            foreach (var file in CryptoFiles)
            {
                if (!file.Error && !file.Bool_IsFolder)
                {
                    await Task.Run(() =>
                    {
                        using (Icon ico = Icon.ExtractAssociatedIcon(file.Str_Path))
                        {
                            file.Icon = ico.ToImageSource();
                        }
                    });
                }
            }

            IsUpdatingCollection = false;
        }

        private void OrderCollectionByFolder()
        {
            ObservableCollection<CryptoFile> FolderFirst =
                new ObservableCollection<CryptoFile>(CryptoFiles.Where(folderName => folderName.Bool_IsFolder).OrderBy(folderName => folderName.Str_FileName).ToList());

            ObservableCollection<CryptoFile> FileSeconds =
                new ObservableCollection<CryptoFile>(CryptoFiles.Where(fileName => !fileName.Bool_IsFolder).OrderBy(fileName => fileName.Str_FileName).ToList());

            CryptoFiles = new ObservableCollection<CryptoFile>(FolderFirst.Concat(FileSeconds).ToList());

        }



        public int Int_TotalFiles { get => _Int_TotalFiles; set { _Int_TotalFiles = value; Notify(); } }
        private int _Int_TotalFiles;

        public int Int_TotalFolders { get => _Int_TotalFolder; set { _Int_TotalFolder = value; Notify(); } }
        private int _Int_TotalFolder;

        public long Long_TotalSize { get => _Long_TotalSize; set { _Long_TotalSize = value; Notify(); } }
        private long _Long_TotalSize;

        public long Long_ProcessedSize { get => _Long_ProcessedSize; set { _Long_ProcessedSize = value; Notify(); } }
        private long _Long_ProcessedSize;

        public TimeSpan Time_Total { get => _Time_Total; set { _Time_Total = value; Notify(); } }
        private TimeSpan _Time_Total;

        public TimeSpan Time_Remaining { get => _Time_Remaining; set { _Time_Remaining = value; Notify(); } }
        private TimeSpan _Time_Remaining;

        public ObservableCollection<CryptoFile> CryptoFiles { get => _CryptoFiles; set { _CryptoFiles = value; Notify(); } }
        private ObservableCollection<CryptoFile> _CryptoFiles = new();



        public void Encrypt()
        {
            if (CryptoFiles != null && CryptoFiles.Count > 0)
            {
                foreach (var file in CryptoFiles)
                {
                    if (!file.Bool_IsFolder)
                    {
                        BackgroundWorker backgroundWorker = new();
                        backgroundWorker.DoWork += (sender, e) =>
                        {
                            file.Vis_ProgressBar = Visibility.Visible;
                            file.Vis_TextBlock = Visibility.Hidden;
                        };
                    }
                }
            }
        }

        

        private DispatcherTimer InternalTimer = new();

        public void StartBackgroundNotify()
        {
            InternalTimer.Interval = TimeSpan.FromMilliseconds(200);
            InternalTimer.Tick += (sender, e) =>
            {
                if (!IsUpdatingCollection)
                {
                    foreach (var file in CryptoFiles)
                        file.Update();
                }
            };
            InternalTimer.Start();
        }
    }
}
