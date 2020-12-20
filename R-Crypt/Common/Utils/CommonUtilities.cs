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

namespace R_Crypt.Common.Utils
{
    public static class CommonUtilities
    {
        public static string GetByteUnit(this long totalByte)
        {
            const double kB = 1024.00;
            const double MB = 1048576.00;
            const double GB = 1073741824.00;

            string temp = "";

            if (totalByte >= kB && totalByte < MB) temp = "kB";
            else if (totalByte >= MB && totalByte < GB) temp = "MB";
            else if (totalByte >= GB) temp = "GB";
            else temp = "B";

            return temp;
        }

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

        public static double ByteConversion(this long totalByte)
        {
            const double kB = 1024.00;
            const double MB = 1048576.00;
            const double GB = 1073741824.00;

            double temp = 0.00;

            if (totalByte >= kB && totalByte < MB) temp = totalByte / kB;
            else if (totalByte >= MB && totalByte < GB) temp = totalByte / MB;
            else if (totalByte >= GB) temp = totalByte / GB;
            else temp = totalByte;

            return temp;
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

        public static string GetSizeFromFilePath(this string path)
        {
            if (File.Exists(path))
            {
                FileInfo info = new FileInfo(path);
                return info.Length.ConvertLongByteToString();
            }
            else return "error";
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

        public static string GetCurrentExePath()
        {
            return Assembly.GetExecutingAssembly().Location;
        }

        public static void CopyExeToPath(string path)
        {
            try
            {
                File.Copy(Assembly.GetExecutingAssembly().Location, path, true);
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
        }
    }
}
