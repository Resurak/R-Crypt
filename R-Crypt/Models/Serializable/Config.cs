using R_Crypt.Common;
using R_Crypt.Models.Base;
using R_Crypt.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace R_Crypt.Models.Serializable
{
    [Serializable]
    public class Config : BaseProgram
    {
        [NonSerialized]
        private DispatcherTimer InternalTimer = new();
        
        public Config()
        {

        }

        public void NotifyChanges()
        {
            InternalTimer.Interval = TimeSpan.FromMilliseconds(50);
            InternalTimer.Tick += (sender, e) =>
            {
                List<string> properties = new();

                foreach (var property in this.GetType().GetProperties().ToList())
                    properties.Add(property.Name);

                Notify(properties);
            };
            InternalTimer.Start();
        }

        public void SaveChanges()
        {
            ConfigHandler.SerializeConfig(this, this.Program_Str_ExePath);
        }

        public string Program_Str_Version { get => _Program_Str_Version; set { _Program_Str_Version = value; } }
        private string _Program_Str_Version = "0.12.0";

        public int Opt_Int_AESKeySize { get => _Opt_Int_AESKeySize; set { _Opt_Int_AESKeySize = value; } }
        private int _Opt_Int_AESKeySize = 256;

        public int Opt_Int_PassSaltSize { get => _Opt_Int_PassSaltSize; set => _Opt_Int_PassSaltSize = value; }
        private int _Opt_Int_PassSaltSize = 32;

        public int Opt_Int_IVKeySize { get => _Opt_Int_IVKeySize; set => _Opt_Int_IVKeySize = value; }
        private int _Opt_Int_IVKeySize = 32;

        public int Opt_Int_RfcIterations { get => _Opt_Int_RfcIterations; set => _Opt_Int_RfcIterations = value; }
        private int _Opt_Int_RfcIterations = 50000;

        public bool Opt_Bool_AutoDeleteFiles { get => _Opt_Bool_AutoDeleteFiles; set => _Opt_Bool_AutoDeleteFiles = value; }
        private bool _Opt_Bool_AutoDeleteFiles = false;

        public bool Opt_Bool_NoUser { get => _Opt_Bool_NoUser; set => _Opt_Bool_NoUser = value; }
        private bool _Opt_Bool_NoUser = false;

        public bool Opt_Bool_RememberUsername { get => _Opt_Bool_RememberUsername; set => _Opt_Bool_RememberUsername = value; }
        private bool _Opt_Bool_RememberUsername = false;

        public string Program_Str_FileSHA256 { get => _Program_Str_FileSHA256; set => _Program_Str_FileSHA256 = value; }
        private string _Program_Str_FileSHA256 = "";

        public string Program_Str_ExePath { get => _Program_Str_ExePath; set => _Program_Str_ExePath = value; }
        private string _Program_Str_ExePath = "";

        public string Program_Str_LastFileSHA256 { get => _Program_Str_LastFileSHA256; set => _Program_Str_LastFileSHA256 = value; }
        private string _Program_Str_LastFileSHA256;

        public string Program_Str_CurrentSHA256 { get => _Program_Str_CurrentSHA256; set => _Program_Str_CurrentSHA256 = value; }
        private string _Program_Str_CurrentSHA256;

        public DateTime Program_Date_LastEncryption { get => _Program_Date_LastEncryption; set => _Program_Date_LastEncryption = value; }
        private DateTime _Program_Date_LastEncryption = DateTime.Now;

        public DateTime Program_Date_LastDecryption { get => _Program_Date_LastDecryption; set => _Program_Date_LastDecryption = value; }
        private DateTime _Program_Date_LastDecryption = DateTime.Now;

        public int Program_Int_NumberOfEncryptedFolders { get => _Program_Int_NumberOfEncryptedFolders; set => _Program_Int_NumberOfEncryptedFolders = value; }
        private int _Program_Int_NumberOfEncryptedFolders = 2;

        public bool Program_Bool_AutoEncryptActive { get => _Program_Bool_AutoEncryptActive; set { _Program_Bool_AutoEncryptActive = value; } }
        private bool _Program_Bool_AutoEncryptActive = false; 

        public string User_Str_LastUser { get => _User_Str_LastUser; set => _User_Str_LastUser = value; }
        private string _User_Str_LastUser = "";

        public DateTime User_Date_LastLogin { get => _User_Date_LastLogin; set => _User_Date_LastLogin = value; }
        private DateTime _User_Date_LastLogin = DateTime.Now;

        public string User_Str_LastUserPassSHA256 { get => _User_Str_LastUserPassSHA256; set => _User_Str_LastUserPassSHA256 = value; }
        private string _User_Str_LastUserPassSHA256 = "";

        public int Stats_Int_FilesEncrypted { get => _Stats_Int_FilesEncrypted; set { _Stats_Int_FilesEncrypted = value; } }
        private int _Stats_Int_FilesEncrypted = 23;

        public int Stats_Int_FilesDecrypted { get => _Stats_Int_FilesDecrypted; set => _Stats_Int_FilesDecrypted = value; }
        private int _Stats_Int_FilesDecrypted = 15;

        public long Stats_Long_TotalSizeEncrypted { get => _Stats_Long_TotalSizeEncrypted; set => _Stats_Long_TotalSizeEncrypted = value; }
        private long _Stats_Long_TotalSizeEncrypted = 1489415418987;

        public long Stats_Long_TotalSizeDecrypted { get => _Stats_Long_TotalSizeDecrypted; set => _Stats_Long_TotalSizeDecrypted = value; }
        private long _Stats_Long_TotalSizeDecrypted = 5489748541;

        public long Stats_Long_BiggestEncrypted { get => _Stats_Long_BiggestEncrypted; set => _Stats_Long_BiggestEncrypted = value; }
        private long _Stats_Long_BiggestEncrypted = 1548935487;

        public long Stats_Long_BiggestDecrypted { get => _Stats_Long_BiggestDecrypted; set { _Stats_Long_BiggestDecrypted = value; } }
        private long _Stats_Long_BiggestDecrypted;

        public string Stats_Str_MostUsedType { get => _Stats_Str_MostUsedType; set => _Stats_Str_MostUsedType = value; }
        private string _Stats_Str_MostUsedType = "Audio";

        public TimeSpan Stats_Time_TotalTimeEncryption { get => _Stats_Time_TotalTimeEncryption; set => _Stats_Time_TotalTimeEncryption = value; }
        private TimeSpan _Stats_Time_TotalTimeEncryption = new TimeSpan(2,23,45);

        public TimeSpan Stats_Time_TotalTimeDecryption { get => _Stats_Time_TotalTimeDecryption; set => _Stats_Time_TotalTimeDecryption = value; }
        private TimeSpan _Stats_Time_TotalTimeDecryption = new TimeSpan(1,54,10);

        public TimeSpan Stats_Time_LongestTimeEncryption { get => _Stats_Time_LongestTimeEncryption; set => _Stats_Time_LongestTimeEncryption = value; }
        private TimeSpan _Stats_Time_LongestTimeEncryption = new TimeSpan(0,23,10);

        public TimeSpan Stats_Time_LongestTimeDecryption { get => _Stats_Time_LongestTimeDecryption; set => _Stats_Time_LongestTimeDecryption = value; }
        private TimeSpan _Stats_Time_LongestTimeDecryption = new TimeSpan(0,32,52);

        public int Stats_Int_CurrentlyEncryptedFiles { get => _Stats_Int_CurrentlyEncryptedFiles; set => _Stats_Int_CurrentlyEncryptedFiles = value; }
        private int _Stats_Int_CurrentlyEncryptedFiles = 15;

        public List<CryptoFile> CryptoFiles_CurrentEncryptedFiles { get => _CryptoFiles_CurrentEncryptedFiles; set => _CryptoFiles_CurrentEncryptedFiles = value; }
        private List<CryptoFile> _CryptoFiles_CurrentEncryptedFiles = new();
    }
}
