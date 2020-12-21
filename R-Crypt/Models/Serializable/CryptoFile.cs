using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using R_Crypt.Common.Utils;
using System.Drawing;
using System.Windows.Media;
using System.Reflection;
using R_Crypt.ViewModels.Base;

namespace R_Crypt.Models.Serializable
{
    [Serializable]
    public class CryptoFile : BaseVM
    {
        public CryptoFile()
        {

        }

        public string Str_Path { get => _Str_Path; set { _Str_Path = value; Notify(); } }
        private string _Str_Path;

        public string Str_FileName { get => _Str_FileName; set { _Str_FileName = value; Notify(); } }
        private string _Str_FileName;


        public ImageSource Icon { get => icon; set { icon = value; Notify(); } }
        private ImageSource icon;


        public bool Bool_IsFolder { get => _Bool_IsFolder; set { _Bool_IsFolder = value; Notify(); } }
        private bool _Bool_IsFolder;

        public string Str_FileExtension { get => _Str_FileExtension; set { _Str_FileExtension = value; Notify(); } }
        private string _Str_FileExtension;

        public string Str_FileType { get => _Str_FileType; set { _Str_FileType = value; Notify(); } }
        private string _Str_FileType;


        public long Long_FileSize { get => _Long_FileSize; set { _Long_FileSize = value; Notify(); } }
        private long _Long_FileSize;

        public string Str_FileSize { get => _Str_FileSize; set { _Str_FileSize = value; Notify(); } }
        private string _Str_FileSize;


        public string Tooltip { get => tooltip; set { tooltip = value; Notify(); } }
        private string tooltip;


        public long Long_ProcessedSize { get => _Long_ProcessedSize; set { _Long_ProcessedSize = value; Notify(); } }
        private long _Long_ProcessedSize;

        public string Str_ProgressTracker { get => _Str_ProgressTracker; set { _Str_ProgressTracker = value; Notify(); } }
        private string _Str_ProgressTracker;


        public string Str_BeforeEncryptionHash { get => _Str_BeforeEncryptionHash; set { _Str_BeforeEncryptionHash = value; Notify(); } }
        private string _Str_BeforeEncryptionHash;

        public string Str_AfterEncryprionHash { get => _Str_AfterEncryprionHash; set { _Str_AfterEncryprionHash = value; Notify(); } }
        private string _Str_AfterEncryprionHash;


        public Visibility Vis_ProgressBar { get => _Vis_ProgressBar; set { _Vis_ProgressBar = value; Notify(); } }
        private Visibility _Vis_ProgressBar;

        public Visibility Vis_TextBlock { get => _Vis_TextBlock; set { _Vis_TextBlock = value; Notify(); } }
        private Visibility _Vis_TextBlock;


        public bool Error { get => error; set { error = value; Notify(); } }
        private bool error;
    }
}
