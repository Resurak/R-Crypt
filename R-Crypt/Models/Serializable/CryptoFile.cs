using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using R_Crypt.Common.Utils;
using System.Drawing;
using System.Windows.Media;
using System.Reflection;
using R_Crypt.ViewModels.Base;

namespace R_Crypt.Models.Serializable
{
    public class CryptoFile : BaseVM
    {
        public CryptoFile()
        {

        }

        public CryptoFile(string file)
        {
            if (file.CheckExist())
            {
                IsFolder = file.IsDirectoryOrFile();

                Path = file;
                PathText = Path.GetFileName();

                Tooltip = $"Path of the file: {Path}";

                if (IsFolder)
                {
                    Icon = new BitmapImage(new Uri(@"pack://application:,,,/"
                        + Assembly.GetExecutingAssembly().GetName().Name
                        + ";component/"
                        + "Resources/folder.png", UriKind.Absolute));

                    TotalBytesString = "";
                    FileTypeText = "Folder";
                }
                else
                {
                    FileInfo info = new(file);

                    FileSize = info.Length;

                    var icon = System.Drawing.Icon.ExtractAssociatedIcon(file);
                    Icon = icon.ToImageSource();

                    TotalBytesString = Path.GetSizeFromFilePath();
                    FileExtension = Path.GetFileExtension();
                    FileTypeText = FileExtension.GetExtensionType();
                }

                ProcessedBytes = 0;
                ProgressText = startText;

                ProgressBarVis = Visibility.Hidden;
                TextProgressVis = Visibility.Visible;
            }
            else
            {
                PathText = "Error";
                Tooltip = $"Error. Cannot get {Path}. Probably need admin privileges";
                Error = true;
            }
        }

        public string Path { get => path; set { path = value; Notify(); } }
        private string path;

        public string PathText { get => pathText; set { pathText = value; Notify(); } }
        private string pathText;


        public ImageSource Icon { get => icon; set { icon = value; Notify(); } }
        private ImageSource icon;


        public bool IsFolder { get => isFolder; set { isFolder = value; Notify(); } }
        private bool isFolder;

        public string FileExtension { get => fileExtension; set { fileExtension = value; Notify(); } }
        private string fileExtension;

        public string FileTypeText { get => fileTypeText; set { fileTypeText = value; Notify(); } }
        private string fileTypeText;


        public long FileSize { get => fileSize; set { fileSize = value; Notify(); } }
        private long fileSize;

        public string TotalBytesString { get => totalBytesString; set { totalBytesString = value; Notify(); } }
        private string totalBytesString;


        public string Tooltip { get => tooltip; set { tooltip = value; Notify(); } }
        private string tooltip;


        public long ProcessedBytes { get => processedBytes; set { processedBytes = value; Notify(); } }
        private long processedBytes;

        public string ProgressText { get => progressText; set { progressText = value; Notify(); } }
        private string progressText;


        public string BeforeEncryptionHash { get => beforeEncryptionHash; set { beforeEncryptionHash = value; Notify(); } }
        private string beforeEncryptionHash;

        public string AfterEncryprionHash { get => afterEncryprionHash; set { afterEncryprionHash = value; Notify(); } }
        private string afterEncryprionHash;


        public Visibility ProgressBarVis { get => progressBarVis; set { progressBarVis = value; Notify(); } }
        private Visibility progressBarVis;

        public Visibility TextProgressVis { get => textProgressVis; set { textProgressVis = value; Notify(); } }
        private Visibility textProgressVis;


        public bool Error { get => error; set { error = value; Notify(); } }
        private bool error;

        private string startText = "Waiting input...";
















    }
}
