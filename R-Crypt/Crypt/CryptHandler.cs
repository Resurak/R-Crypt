using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using R_Crypt.Crypt.Base;

namespace R_Crypt.Crypt
{
    public class CryptHandler : CryptBase
    {
        public delegate void CryptProgressChangedEventHandler(object sender, CryptProgress e);
        public event CryptProgressChangedEventHandler CryptProgressChanged;
        public event CryptProgressChangedEventHandler CryptCompleted;

        private protected void OnStartingCrypt()
        {
            progress.StartTimer();
        }

        private protected void OnProcessingCrypt()
        {
            CryptProgressChanged?.Invoke(this, progress);
        }

        private protected void OnCompletedCrypt()
        {
            progress.Completed = true;
            progress.StopTimer();

            CryptCompleted?.Invoke(this, progress);
        }



        public CryptHandler()
        {
            if (progress != null) progress = null;
        }



        CryptProgress progress;

        public int SaltSize { get => _SaltSize; set => _SaltSize = value; }
        private int _SaltSize = Default_SaltSize;

        public int IVKeySize { get => _IVKeySize; set => _IVKeySize = value; }
        private int _IVKeySize = Default_IVSize;

        public int RfcIterations { get => _RfcIterations; set => _RfcIterations = value; }
        private int _RfcIterations = Default_RfcIterations;

        public byte[] StreamBuffer { get => _StreamBuffer; set => _StreamBuffer = value; }
        private byte[] _StreamBuffer = new byte[1048576];



        public async void EncryptFileAsync(string pass, string path)
        {
            progress = new CryptProgress(path);

            var salt = GenerateSalt(SaltSize);
            var ivKey = GenerateSalt(IVKeySize);

            OnStartingCrypt();

            using (var saltedPassword = new Rfc2898DeriveBytes(pass, salt, RfcIterations))
            {
                var saltedPasswordBytes = saltedPassword.GetBytes(SaltSize);
                await EncryptInternalAsync(saltedPasswordBytes, salt, ivKey, path, path);
            }

            OnCompletedCrypt();
        }

        public async void DecryptFileAsync(string pass, string path)
        {
            progress = new CryptProgress(path);

            var salt = new byte[SaltSize];
            var ivKey = new byte[IVKeySize];

            using (var streamFileToDecrypt = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await streamFileToDecrypt.ReadAsync(salt, 0, salt.Length);
                await streamFileToDecrypt.ReadAsync(ivKey, 0, ivKey.Length);
                streamFileToDecrypt.Close();
            }

            OnStartingCrypt();

            using (var saltedPassword = new Rfc2898DeriveBytes(pass, salt, RfcIterations))
            {
                var saltedPasswordBytes = saltedPassword.GetBytes(SaltSize);
                await DecryptInternalAsync(saltedPasswordBytes, ivKey, path, path);
            }

            OnCompletedCrypt();
        }

        public async void EncryptBytesToFile(string pass, byte[] bytes, string path)
        {

        }



        private async Task EncryptInternalAsync(byte[] saltedPass, byte[] salt, byte[] ivKey, string fileToEncrypt, string outputFile)
        {
            if (fileToEncrypt == outputFile) outputFile = outputFile + ".aes";
            
            using(var AES_Key = new RijndaelManaged { BlockSize = Default_AES_BlockSize, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
            using(var encryptor = AES_Key.CreateEncryptor(saltedPass, ivKey))
            using(var streamEncryptedFile = new FileStream(fileToEncrypt, FileMode.Open, FileAccess.Read, FileShare.Read))
            using(var streamOutputFile = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using(var streamCrypto = new CryptoStream(streamOutputFile, encryptor, CryptoStreamMode.Write))
            {
                try
                {
                    streamOutputFile.Write(salt, 0, salt.Length);
                    streamOutputFile.Write(ivKey, 0, ivKey.Length);

                    long remaining = progress.TotalByte;

                    do
                    {
                        int bytesRead = await streamEncryptedFile.ReadAsync(StreamBuffer, 0, (int)Math.Min(StreamBuffer.Length, remaining));
                        if (bytesRead == 0) break;

                        await streamCrypto.WriteAsync(StreamBuffer, 0, bytesRead);

                        progress.CurrentByte = streamEncryptedFile.Position;
                        OnProcessingCrypt();

                        remaining -= bytesRead;
                    }
                    while (remaining > 0);

                    streamCrypto.FlushFinalBlock();
                    streamCrypto.Close();
                } 
                catch (Exception e) { MessageBox.Show(e.ToString()); }
                finally
                {
                    streamEncryptedFile.Close();
                    streamOutputFile.Close();
                }
            }
        }

        private async Task DecryptInternalAsync(byte[] saltedPass, byte[] IVkey, string fileToDecrypt, string fileOutput)
        {
            if (fileToDecrypt == fileOutput && fileToDecrypt.Contains(".aes")) fileOutput = fileOutput.Replace(".aes", "");

            using (var AES_Key = new RijndaelManaged { BlockSize = Default_AES_BlockSize, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
            using (var decryptor = AES_Key.CreateDecryptor(saltedPass, IVkey))
            using (var streamToDecrypt = new FileStream(fileToDecrypt, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var streamOutput = new FileStream(fileOutput, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var cryptostream = new CryptoStream(streamToDecrypt, decryptor, CryptoStreamMode.Read))
            {
                try
                {
                    streamToDecrypt.Seek((SaltSize + IVKeySize), SeekOrigin.Begin);

                    long remaining = progress.TotalByte;

                    do
                    {
                        int bytesRead = await cryptostream.ReadAsync(StreamBuffer, 0, (int)Math.Min(StreamBuffer.Length, remaining));
                        if (bytesRead == 0) break;

                        await streamOutput.WriteAsync(StreamBuffer, 0, bytesRead);

                        progress.CurrentByte = streamToDecrypt.Position;
                        OnProcessingCrypt();

                        remaining -= bytesRead;
                    }
                    while (remaining > 0);
                } 
                catch (Exception e) { MessageBox.Show(e.ToString()); }
                finally
                {
                    streamOutput.Close();
                    streamToDecrypt.Close();
                }
            }

        }
    }
}
