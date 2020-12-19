using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace R_Crypt.Models.Serializable
{
    [Serializable]
    public class Config
    {
        public Config()
        {

        }

        public string ProgramVersion { get => _ProgramVersion; set { _ProgramVersion = value; } }
        private string _ProgramVersion = "0.10.0";

        public int AES_KeySize { get => _AES_KeySize; set { _AES_KeySize = value; } }
        private int _AES_KeySize = 256;

        public int SaltSize { get => _SaltSize; set => _SaltSize = value; }
        private int _SaltSize = 32;

        public int IVKeySize { get => _IVKeySize; set => _IVKeySize = value; }
        private int _IVKeySize = 32;

        public int RfcIterations { get => _RfcIterations; set => _RfcIterations = value; }
        private int _RfcIterations = 50000;

        public bool AutoDeleteFiles { get => _AutoDeleteFiles; set => _AutoDeleteFiles = value; }
        private bool _AutoDeleteFiles = false;

        public bool NoUser { get => _NoUser; set => _NoUser = value; }
        private bool _NoUser = false;

        public bool RememberLastUserUsername { get => _RememberLastUserUsername; set => _RememberLastUserUsername = value; }
        private bool _RememberLastUserUsername = false;

        public string ProgramSHA256 { get => _ProgramSHA256; set => _ProgramSHA256 = value; }
        private string _ProgramSHA256 = "";

        public string ExePath { get => _ExePath; set => _ExePath = value; }
        private string _ExePath = "";

        public string LastExeSha256 { get; set; }
        public string CurrentExeSha256 { get; set; }

        public string LastUser { get => _LastUser; set => _LastUser = value; }
        private string _LastUser = "";

        public DateTime LastUserLogin { get => _LastUserLogin; set => _LastUserLogin = value; }
        private DateTime _LastUserLogin;

        public string LastUserPasswordHash { get => _LastUserPasswordHash; set => _LastUserPasswordHash = value; }
        private string _LastUserPasswordHash = "";
    }
}
