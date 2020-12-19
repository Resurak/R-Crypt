using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
