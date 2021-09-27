using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Botaemic.SaveAndLoad
{
    /// <summary>
    /// This class implements methods to encrypt and decrypt streams
    /// </summary>
    public abstract class SaveAndLoadEncrypter
    {
        /// <summary>
        /// The Key to use to save and load the file
        /// </summary>
        public string Key { get; set; } = "DefaultKey";

        protected string _saltText = "WHOOTWHOOT";

        /// <summary>
        /// Encrypts the specified input stream into the specified output stream using the key passed in parameters
        /// </summary>
        protected virtual void Encrypt(Stream inputStream, Stream outputStream, string sKey)
        {
            RijndaelManaged algorithm = new RijndaelManaged();
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sKey, Encoding.ASCII.GetBytes(_saltText));

            algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
            algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

            var cryptostream = new CryptoStream(inputStream, algorithm.CreateEncryptor(), CryptoStreamMode.Read);
            cryptostream.CopyTo(outputStream);
        }

        /// <summary>
        /// Decrypts the input stream into the output stream using the key passed in parameters
        /// </summary>
        protected virtual void Decrypt(Stream inputStream, Stream outputStream, string sKey)
        {
            RijndaelManaged algorithm = new RijndaelManaged();
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sKey, Encoding.ASCII.GetBytes(_saltText));

            algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
            algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

            var cryptostream = new CryptoStream(inputStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read);
            cryptostream.CopyTo(outputStream);
        }
    }
}