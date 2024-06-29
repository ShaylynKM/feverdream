using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BayatGames;
using BayatGames.SaveGameFree;

public class SaveManager : MonoBehaviour
{
    private string _savedScene;

    private MusicManager _musicManager;

    [SerializeField]
    private bool _save;

    public void OnSave()
    {
        if(_save == true)
        {
            _savedScene = SceneManager.GetActiveScene().name; // Saved scene is this scene
            SaveGame.Save<string>("_savedScene", _savedScene);
        }
    }
    public void OnLoadGame()
    {

    }
}
