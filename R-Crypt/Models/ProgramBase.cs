﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using R_Crypt.Models.Serializable;

namespace R_Crypt.Models
{
    public class ProgramBase
    {
        public ProgramBase()
        {
            SetDefaults();
        }

        public static Config ProgramWideConfig { get; set; }

        public string Program_Name = "R-Crypt";
        public string Program_ExeName = "R-Crypt.exe";
        public string Program_Extension = ".crypto";
        public string ConfigFile_Name = "config.rcfg";

        public string ConfigFolder_Path { get => _ConfigFolder_Path; }
        private string _ConfigFolder_Path = "";

        public string ConfigFile_Path { get => _ConfigFile_Path; }
        private string _ConfigFile_Path =  "";

        public string ExeFile_Path { get => _ExeFilePath; }
        private string _ExeFilePath = "";

        public string CurrentExePath { get => Assembly.GetExecutingAssembly().Location; }

        private void SetDefaults()
        {
            _ConfigFolder_Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Program_Name);
            _ConfigFile_Path = Path.Combine(ConfigFolder_Path, ConfigFile_Name);
            _ExeFilePath = Path.Combine(ConfigFolder_Path, Program_ExeName);
        }
    }
}
