using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace R_Crypt.Crypt.Base
{
    public class CryptBase
    {
        public CryptBase()
        {

        }

        public const int Default_AES_BlockSize = 256;
        public const int Default_SaltSize = 32;
        public const int Default_IVSize = 32;
        public const int Default_RfcIterations = 50000;

        public static byte[] GenerateSalt(int size)
        {
            byte[] salt = new byte[size];

            using (var rng = new RNGCryptoServiceProvider()) rng.GetBytes(salt);

            return salt;
        }
    }
}
