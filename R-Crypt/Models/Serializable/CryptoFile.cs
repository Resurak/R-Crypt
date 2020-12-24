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
using R_Crypt.Models.Base;

namespace R_Crypt.Models.Serializable
{
    [Serializable]
    public class CryptoFile : BaseModel
    {
        internal void Update()
        {
            UpdateAll();
        }

        /// <summary>
        /// The path of the file
        /// </summary>
        public string Str_Path { get => _Str_Path; set => _Str_Path = value; }
        private string _Str_Path;

        /// <summary>
        /// The name of the file. ie : Shish.exe
        /// </summary>
        public string Str_FileName { get => _Str_FileName; set  => _Str_FileName = value; }
        private string _Str_FileName;

        /// <summary>
        /// The icon extracted. if its a folder then give a simple icon
        /// </summary>
        public ImageSource Icon { get => icon; set => icon = value; }
        private ImageSource icon;

        /// <summary>
        /// True if its a folder, false if its a file
        /// </summary>
        public bool Bool_IsFolder { get => _Bool_IsFolder; set => _Bool_IsFolder = value; }
        private bool _Bool_IsFolder;

        /// <summary>
        /// The extension of the file
        /// </summary>
        public string Str_FileExtension { get => _Str_FileExtension; set => _Str_FileExtension = value; }
        private string _Str_FileExtension;

        /// <summary>
        /// The type of the file. ie: Audio, Video etc
        /// </summary>
        public string Str_FileType { get => _Str_FileType; set => _Str_FileType = value; }
        private string _Str_FileType;

        /// <summary>
        /// The Size of the file
        /// </summary>
        public long Long_FileSize { get => _Long_FileSize; set => _Long_FileSize = value; }
        private long _Long_FileSize;

        /// <summary>
        /// Size of the file in string. Probably to remove
        /// </summary>
        public string Str_FileSize { get => _Str_FileSize; set => _Str_FileSize = value; }
        private string _Str_FileSize;


        public string Tooltip { get => tooltip; set => tooltip = value; }
        private string tooltip;


        public long Long_ProcessedSize { get => _Long_ProcessedSize; set => _Long_ProcessedSize = value; }
        private long _Long_ProcessedSize;

        public string Str_ProgressTracker { get => _Str_ProgressTracker; set => _Str_ProgressTracker = value; }
        private string _Str_ProgressTracker;


        public string Str_BeforeEncryptionHash { get => _Str_BeforeEncryptionHash; set => _Str_BeforeEncryptionHash = value; }
        private string _Str_BeforeEncryptionHash;

        public string Str_AfterEncryprionHash { get => _Str_AfterEncryprionHash; set => _Str_AfterEncryprionHash = value; }
        private string _Str_AfterEncryprionHash;


        public Visibility Vis_ProgressBar { get => _Vis_ProgressBar; set => _Vis_ProgressBar = value; }
        private Visibility _Vis_ProgressBar;

        public Visibility Vis_TextBlock { get => _Vis_TextBlock; set => _Vis_TextBlock = value; }
        private Visibility _Vis_TextBlock;


        public bool Error { get => error; set => error = value; }
        private bool error;
    }
}
