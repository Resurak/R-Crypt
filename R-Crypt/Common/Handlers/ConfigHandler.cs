using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using R_Crypt.Models;
using R_Crypt.Models.Base;
using R_Crypt.Models.Serializable;

namespace R_Crypt.Common.Handlers
{
    public class ConfigHandler : BaseModel
    {

        public static void SerializeConfig(Config config, string path)
        {
            using (var filestream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                BinaryFormatter formatter = new();
                formatter.Serialize(filestream, config);
                filestream.Close();
            }
        }

        public static Config DeserializeConfig(string path)
        {
            Config config = null;

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BinaryFormatter formatter = new();
                config = (Config)formatter.Deserialize(fileStream);
                fileStream.Close();
            }

            return config;
        }


        public ConfigHandler()
        {
            BaseProgram baseProgram = new();
            MainConfig = ConfigHandler.DeserializeConfig(baseProgram.Base_Path_ConfigFile);
        }


        public Config MainConfig { get => _MainConfig; set { _MainConfig = value; Notify(); } }
        private Config _MainConfig = new();

        private DispatcherTimer InternalTimer = new();

        public void SaveChanges()
        {
            ConfigHandler.SerializeConfig(MainConfig, MainConfig.Program_Str_CurrentExePath);
        }
    }
}
