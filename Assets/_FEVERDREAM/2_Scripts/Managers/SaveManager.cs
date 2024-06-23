using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BayatGames;
using BayatGames.SaveGameFree;

public class SaveManager : MonoBehaviour
{
    // Things to save:
    // Scene name
    // Music manager
    // SFX manager (if there is one)

    // how tf do i save a gameobject or a script

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

            _musicManager = FindObjectOfType<MusicManager>(); // Music manager is the one in this scene

            if(_musicManager != null)
            {
                // ???????????????????????????????????
            }
        }
    }
}
