using System.IO;

namespace Botaemic.SaveAndLoad
{
    /// <summary>
    /// Iinterface to implement save and load using different methods (binary, json, etc)
    /// </summary>
    public interface ISaveLoadControllerMethod
    {
        void Save(object objectToSave, FileStream saveFile);
        object Load(System.Type objectType, FileStream saveFile);
    }
}
