using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Crypt.Models.Serializable
{
    public class CryptoFiles
    {
        public CryptoFiles()
        {

        }

        public string Path { get; set; }
        public string TotalBytes { get; set; }
        public double ProcessedBytes { get; set; }
        public string BeforeEncryptionHash { get; set; }
        public string AfterEncryprionHash { get; set; }
    }
}
