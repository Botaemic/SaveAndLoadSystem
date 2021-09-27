using System.IO;
using UnityEngine;

namespace Botaemic.SaveAndLoad
{
	/// <summary>
	/// The possible methods to save and load files to and from disk available in the SaveAndLoadController
	/// </summary>
	public enum SaveLoadControllerMethods { BinaryEncrypted, Json }; //Only one method for now

	public static class SaveAndLoadController
    {
        public static ISaveLoadControllerMethod saveLoadMethod = new SaveAndLoadControllerMethodBinaryEncrypted();
        
		private const string _baseFolderName = "/Data/";
        private const string _defaultFolderName = "SaveAndLoadController";

		/// <summary>
		/// Save the specified saveObject, fileName and foldername into a file on disk.
		/// </summary>
		public static void Save(object saveObject, string fileName, string foldername = _defaultFolderName)
		{
			string savePath = DetermineSavePath(foldername);
			string saveFileName = DetermineSaveFileName(fileName);

			if (!Directory.Exists(savePath))
			{
				Directory.CreateDirectory(savePath);
			}

			FileStream saveFile = File.Create(savePath + saveFileName);

			saveLoadMethod.Save(saveObject, saveFile);
			saveFile.Close();
		}

		/// <summary>
		/// Load the specified file based on a file name into a specified folder
		/// </summary>
		public static object Load(System.Type objectType, string fileName, string foldername = _defaultFolderName)
		{
			string savePath = DetermineSavePath(foldername);
			string saveFileName = savePath + DetermineSaveFileName(fileName);

			object returnObject;

			if (!Directory.Exists(savePath) || !File.Exists(saveFileName))
			{
				return null;
			}

			FileStream saveFile = File.Open(saveFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			returnObject = saveLoadMethod.Load(objectType, saveFile);
			saveFile.Close();

			return returnObject;
		}

		/// <summary>
		/// Determines the save path to use when loading and saving a file based on a folder name.
		/// </summary>
		static string DetermineSavePath(string folderName = _defaultFolderName)
		{
			string savePath;
			// depending on the device we're on, we assemble the path
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				savePath = Application.persistentDataPath + _baseFolderName;
			}
			else
			{
				savePath = Application.persistentDataPath + _baseFolderName;
			}
#if UNITY_EDITOR
			savePath = Application.dataPath + _baseFolderName;
#endif

			savePath = savePath + folderName + "/";
			return savePath;
		}

		/// <summary>
		/// Determines the name of the file to save
		/// </summary>
		static string DetermineSaveFileName(string fileName)
		{
			return fileName;
		}


	}
}