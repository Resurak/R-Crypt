using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using R_Crypt.Models;

namespace R_Crypt.Common
{
    public class ConfigHandler
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

    }
}
