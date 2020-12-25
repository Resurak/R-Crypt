using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static R_Crypt.Models.Serializable.CryptoFile;

namespace R_Crypt.Common.Utils
{
    public static class CommonUtilities
    {
        public static string ConvertLongByteToString(this long totalByte)
        {
            const double kB = 1024.00;
            const double MB = 1048576.00;
            const double GB = 1073741824.00;

            string tempStr = "";

            double tempByte = 0.00;

            if (totalByte >= kB && totalByte < MB) tempByte = totalByte / kB;
            else if (totalByte >= MB && totalByte < GB) tempByte = totalByte / MB;
            else if (totalByte >= GB) tempByte = totalByte / GB;
            else tempByte = totalByte;

            if (totalByte >= kB && totalByte < MB) tempStr = $"{Math.Round(tempByte, 2)} kB";
            else if (totalByte >= MB && totalByte < GB) tempStr = $"{Math.Round(tempByte, 2)} MB";
            else if (totalByte >= GB) tempStr = $"{Math.Round(tempByte, 2)} GB";
            else tempStr = $"{Math.Round(tempByte, 2)} B";

            return tempStr;
        }

        public static string CheckFileSHA256(string filePath)
        {
            byte[] hash;
            SHA256 sha256 = SHA256.Create();
            StringBuilder sb = new();

            using (var stream = new FileStream(filePath, FileMode.Open ,FileAccess.Read, FileShare.Read))
            {
                hash = sha256.ComputeHash(stream);
                stream.Close();
            }

            foreach (var b in hash) sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        public static long GetSizeFromFilePath(this string path)
        {
            if (File.Exists(path))
            {
                FileInfo info = new FileInfo(path);
                return info.Length;
            }
            else return -1;
        }

        public static string GetFileName(this string path)
        {
            return path.Substring(path.LastIndexOf('\\') + 1);
        }

        public static string GetFileExtension(this string path)
        {
            return Path.GetExtension(path);
        }

        public static ImageSource ToImageSource(this Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            imageSource.Freeze();

            return imageSource;
        }

        /// <summary>
        /// Return false if its a file, true if its a folder
        /// </summary>
        /// <param name="path">the path to check</param>
        /// <returns></returns>
        public static bool IsDirectoryOrFile(this string path)
        {
            FileAttributes attributes = File.GetAttributes(path);
            if ((attributes & FileAttributes.Directory) == FileAttributes.Directory) return true;
            else return false;
        }

        /// <summary>
        /// Check if a path exist
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CheckExist(this string path)
        {
            bool checkFile = File.Exists(path);
            bool checkDir = Directory.Exists(path);

            if (checkFile || checkDir) return true;
            else return false;
        }

        public static void CopyExeToPath(string path)
        {
            try
            {
                File.Copy(Assembly.GetExecutingAssembly().Location, path, true);
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
        }

        public static string GetTypeString(this string extension)
        {
            FileType type = extension.GetExtensionType();

            switch (type)
            {
                case FileType.Audio:
                    return ExtensionType.str_audio;
                case FileType.Video:
                    return ExtensionType.str_video;
                case FileType.Audio_Video:
                    return ExtensionType.str_audio_video;
                case FileType.Image:
                    return ExtensionType.str_image;
                case FileType.MSWord:
                    return ExtensionType.str_word;
                case FileType.MSExcel:
                    return ExtensionType.str_excel;
                case FileType.PDF:
                    return ExtensionType.str_pdf;
                case FileType.Exe:
                    return ExtensionType.str_exe;
                case FileType.Rar:
                    return ExtensionType.str_rar;
                case FileType.Zip:
                    return ExtensionType.str_zip;
                case FileType.DLL:
                    return ExtensionType.str_dll;
                case FileType.Text:
                    return ExtensionType.str_text;
                case FileType.Link:
                    return ExtensionType.str_link;
                case FileType.NotSupported:
                    break;
            }
            return extension;
        }
    }
}