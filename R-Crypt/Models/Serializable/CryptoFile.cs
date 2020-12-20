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

namespace R_Crypt.Models.Serializable
{
    public class CryptoFile
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

        public string Path { get; set; }
        public string PathText { get; set; }

        public ImageSource Icon { get; set; } 

        public bool IsFolder { get; set; }

        public string FileExtension { get; set; }
        public string FileTypeText { get; set; }

        public long FileSize { get; set; }
        public string TotalBytesString { get; set; }

        public string Tooltip { get; set; }

        public double ProcessedBytes { get; set; }
        public string ProgressText { get; set; }

        public string BeforeEncryptionHash { get; set; }
        public string AfterEncryprionHash { get; set; }

        public Visibility ProgressBarVis { get; set; }
        public Visibility TextProgressVis { get; set; }

        public bool Error { get; set; }
        private string startText = "Waiting input...";
    }
}
