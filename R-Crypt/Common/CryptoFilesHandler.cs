using R_Crypt.Common.Utils;
using R_Crypt.Models.Serializable;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

namespace R_Crypt.Common
{
    public static class CryptoFilesHandler
    {
        public static CryptoFile GetCryptoFile(this string file)
        {
            CryptoFile crypto = new();

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

                    crypto.Str_FileSize = "";
                    crypto.Str_FileType = "Folder";
                }
                else
                {
                    FileInfo info = new(file);

                    crypto.Long_FileSize = info.Length;

                    var icon = System.Drawing.Icon.ExtractAssociatedIcon(file);
                    crypto.Icon = icon.ToImageSource();

                    crypto.Str_FileSize = crypto.Str_Path.GetSizeFromFilePath();
                    crypto.Str_FileExtension = crypto.Str_Path.GetFileExtension();
                    crypto.Str_FileType = crypto.Str_FileExtension.GetExtensionType();
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

            return crypto;
        }
    }
}
