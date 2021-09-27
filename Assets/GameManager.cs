using Botaemic.SaveAndLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings = null;
    [SerializeField] private SaveAndLoad _saveFile = new SaveAndLoad();

    public GameSettings GameSettings { get => _gameSettings; private set => _gameSettings = value; }

    private void Awake()
    {
        GameSettings = (GameSettings)_saveFile.Load();
        if (GameSettings == null)
        {
            GameSettings = new GameSettings(); // default name for now, could be replaced with google ID or iOs id
            _saveFile.Save(GameSettings);
        }
    }

    private void OnApplicationQuit()
    {
        _saveFile.Save(GameSettings);
    }
}
