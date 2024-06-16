using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    private int _firstSceneIndex = 1;

    [SerializeField]
    private Canvas _mainMenuCanvas;

    //[SerializeField]
    //private GameObject _settingsMenu;

    [SerializeField]
    private GameObject _loadSaveMenu;

    private void Awake()
    {
        //_settingsMenu.SetActive(false);
        _loadSaveMenu.SetActive(false);
    }

    public void OnNewGame()
    {
        SceneManager.LoadScene(_firstSceneIndex); // Loads the specified scene
    }

    public void OnContinue()
    {
        Debug.Log("load game");
    }


    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }

    //public void CloseMenus()
    //{
    //    if(_loadSaveMenu.activeInHierarchy)
    //    {
    //        _loadSaveMenu.SetActive(false);

    //        // For closing menus within the main menu
    //    }
    //}

}
