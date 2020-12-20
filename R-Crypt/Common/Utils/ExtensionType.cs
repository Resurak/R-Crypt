using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Crypt.Common.Utils
{
    public static class ExtensionType
    {
        public static string[] Audio = new string[]
        {
            ".3gp", ".aa", ".aac", ".aax", ".act", ".aiff", ".alac", ".amr", ".ape", ".au", ".awb", ".dct",
            ".dss", ".dvf", ".flac", ".gsm", ".iklax", ".ivs", ".m4a", ".m4b", ".mmf", ".mp3", ".mpc", ".msv",
            ".nmf", ".oga", ".mogg", ".opus", ".org", ".ra", ".rm", ".rf64", ".sln", ".tta", ".voc",
            ".vox", ".wav", ".wma", ".cda"
        };

        public static string[] Video = new string[]
        {
            ".mkv", ".flv", ".vob", ".ogv", ".drc", ".gifv", "mng", ".avi", ".mts", ".m2ts", ".ts", ".mov", ".qt",
            ".wmv", ".yuv", ".rm", ".rmvb", ".viv", ".asf", ".amv", ".mp4", ".m4p", ".m4v", ".mpg", ".mp2", "mpeg",
            ".mpe", ".mpv", ".m4v", ".svi", ".3gp", ".3g2", ".mxf", ".roq", ".nsv", ".f4v", ".f4p", "f4a", "f4b"
        };

        public static string[] Audio_Video = new string[] { ".webm", ".ogg" };

        public static string[] Image = new string[]
        {
            ".jpg", ".jpeg", ".jpe", ".jif", ".jfif", ".jfi", ".png", ".gif", ".webp", ".tiff", ".tif", ".psd",
            ".bmp", ".dib", ".heif", ".heic", ".ind", ".indd", ".indt", "jp2", "j2k", ".jpf", ".jpx", ".jpm", "mj2",
            ".svg", ".svgz", ".ai", ".eps"
        };

        public static string PDF = ".pdf";

        public static string[] MSWord = new string[]
        {
            ".doc", ".dot", ".wbk", ".docx", ".docm", ".dotx", ".dotm", ".docb"
        };

        public static string[] MSExcel = new string[]
        {
            ".xls", ".xlt", ".xlm", ".xlsx", ".xlsm", ".xltx", ".xltm", ".xlsb", ".xla", ".xlam", ".xll", ".xlw"
        };

        public static string Text = ".txt";

        public static string Exe = ".exe";
        public static string DLL = ".dll";
        public static string Rar = ".rar";
        public static string Zip = ".zip";

        public static string GetExtensionType(this string extension)
        {
            foreach (var str in Audio)
            {
                if (extension == str) return "Audio";
            }

            foreach (var str in Video)
            {
                if (extension == str) return "Video";
            }

            foreach (var str in Audio_Video)
                if (extension == str) return "Multimedia";

            foreach (var str in Image)
                if (extension == str) return "Image";

            foreach (var str in MSWord)
                if (extension == str) return "Word Document";

            foreach (var str in MSExcel)
                if (extension == str) return "Excel Document";

            if (extension == PDF) return "PDF Document";
            if (extension == Exe) return "Program";
            if (extension == Rar) return "Rar Archive";
            if (extension == Zip) return "Compressed Folder";
            if (extension == DLL) return "DLL Library";
            if (extension == Text) return "Text File";

            return extension;
        }
    }
}
