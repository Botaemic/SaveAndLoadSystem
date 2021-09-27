using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


namespace Botaemic.SaveAndLoad
{
    public class SaveAndLoadControllerMethodJson : ISaveLoadControllerMethod
    {
        /// <summary>
        /// Saves the specified object at the specified location after converting it to json
        /// </summary>
        public void Save(object objectToSave, FileStream saveFile)
        {
            string json = JsonUtility.ToJson(objectToSave);
            StreamWriter streamWriter = new StreamWriter(saveFile);
            streamWriter.Write(json);
            streamWriter.Close();
            saveFile.Close();
        }

        /// <summary>
        /// Loads the specified file and decodes it
        /// </summary>
        public object Load(System.Type objectType, FileStream saveFile)
        {
            object savedObject; // = System.Activator.CreateInstance(objectType);
            StreamReader streamReader = new StreamReader(saveFile, Encoding.UTF8);
            string json = streamReader.ReadToEnd();
            savedObject = JsonUtility.FromJson(json, objectType);
            streamReader.Close();
            saveFile.Close();
            return savedObject;
        }
    }
}

