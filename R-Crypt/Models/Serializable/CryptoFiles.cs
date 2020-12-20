using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_Crypt.Common.Utils;

namespace R_Crypt.Models.Serializable
{
    public class CryptoFiles
    {
        public CryptoFiles()
        {

        }

        public CryptoFiles(string file)
        {
            if (file.CheckExist())
            {
                Path = file;
                IsFolder = file.IsDirectoryOrFile();

                if (IsFolder) TotalBytes = "folder";
                else TotalBytes = file.GetSizeFromFilePath();

                ProcessedBytes = 0;
                ProgressText = startText;
            }
            else Error = true;
        }

        public string Path { get; set; }
        public bool IsFolder { get; set; }
        public string TotalBytes { get; set; }
        public double ProcessedBytes { get; set; }
        public string ProgressText { get; set; }
        public string BeforeEncryptionHash { get; set; }
        public string AfterEncryprionHash { get; set; }

        public bool Error { get; set; }
        private string startText = "Waiting input...";
    }
}
