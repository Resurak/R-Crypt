using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Crypt.Common.Utils
{
    public static class Utils
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
    }
}
