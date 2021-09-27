using UnityEngine;

namespace Botaemic.SaveAndLoad
{
    [System.Serializable]
    public class GameSettings
    {
        public string name = "Empty Game";

        public GameSettings()
        {
            name = "default game";
        }

        public GameSettings(string name)
        {
            this.name = name;
        }

        public GameSettings(GameSettings playerSettings)
        {
            this.name = playerSettings.name;
        }
    }


    [System.Serializable]
    public class SaveAndLoad 
    {
        #region Inspector
        [Header("Saved object")]
        public GameSettings savedGame;

        [Header("Save settings")]
        [Tooltip("The chosen save method (json, encrypted binary)")]
        [SerializeField] private SaveLoadControllerMethods _saveLoadMethod = SaveLoadControllerMethods.BinaryEncrypted;
        [Tooltip("The name of the file to save")]
        [SerializeField] private string _fileName = "gamesettings";
        [Tooltip("The name of the destination folder")]
        [SerializeField] private string _folderName = "save/";
        [Tooltip("The extension to use")]
        [SerializeField] private string _saveFileExtension = ".sav";
        [Tooltip("The key to use to encrypt the file (if needed)")]
        [SerializeField] private string _encryptionKey = "TheKeyIs42";

        #endregion

        #region Private Variables
        private ISaveLoadControllerMethod _saveLoadControllerMethod;
        #endregion

        public SaveAndLoad()
        {

        }

        /// <summary>
        /// Saves the contents of the Object into a file
        /// </summary>
        public void Save(object saveObject)
        {
            savedGame = saveObject as GameSettings;
            InitializeSaveLoadMethod();
            SaveAndLoadController.Save(saveObject, _fileName + _saveFileExtension, _folderName);
        }

        /// <summary>
        /// Loads the saved data
        /// </summary>
        public object Load()
        {
            InitializeSaveLoadMethod();
            savedGame = (GameSettings)SaveAndLoadController.Load(typeof(GameSettings), _fileName + _saveFileExtension, _folderName);
            return savedGame;
        }

        /// <summary>
        /// Creates a new SaveAndLoadControllerMethod and passes it to the SaveAndLoadController
        /// </summary>
        private void InitializeSaveLoadMethod()
        {
            // Only one methode available right now.
            switch (_saveLoadMethod)
            {
                case SaveLoadControllerMethods.BinaryEncrypted:
                {
                    _saveLoadControllerMethod = new SaveAndLoadControllerMethodBinaryEncrypted();
                    (_saveLoadControllerMethod as SaveAndLoadEncrypter).Key = _encryptionKey;
                    break;
                }
                case SaveLoadControllerMethods.Json:
                {
                    _saveLoadControllerMethod = new SaveAndLoadControllerMethodJson();
                    break;
                }
            }
            SaveAndLoadController.saveLoadMethod = _saveLoadControllerMethod;
        }
    }
}