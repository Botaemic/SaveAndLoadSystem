using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using UnityEngine;

namespace Botaemic.SaveAndLoad
{
    //TODO RENAME to SaveAndLoadControllerMethodBinaryEncrypted
    class SaveAndLoadControllerMethodBinaryEncrypted : SaveAndLoadEncrypter, ISaveLoadControllerMethod
    {
        /// <summary>
        /// Saves the specified object to disk at the specified location after encrypting it 
        /// </summary>
        public void Save(object objectToSave, FileStream saveFile)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, objectToSave);
            memoryStream.Position = 0;
            Encrypt(memoryStream, saveFile, Key);

            saveFile.Flush();
            memoryStream.Close();
            saveFile.Close();
        }

        /// <summary>
        /// Loads the specified file from disk, decrypts it, and deserializes it
        /// </summary>
        public object Load(System.Type objectType, FileStream saveFile)
        {
            object savedObject;
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                Decrypt(saveFile, memoryStream, Key);
            }
            catch (CryptographicException ce)
            {
                Debug.LogError("[SaveAndLoadController] Encryption key error: " + ce.Message);
                return null;
            }
            memoryStream.Position = 0;
            savedObject = formatter.Deserialize(memoryStream);
            memoryStream.Close();
            saveFile.Close();
            return savedObject;
        }
    }
}
