using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    private int _firstSceneIndex = 1;

    [SerializeField]
    private Canvas _mainMenuCanvas;

    [SerializeField]
    private GameObject _settingsMenu;

    [SerializeField]
    private GameObject _loadSaveMenu;

    private void Awake()
    {
        _settingsMenu.SetActive(false);
        _loadSaveMenu.SetActive(false);
    }

    public void OnNewGame()
    {
        SceneManager.LoadScene(_firstSceneIndex); // Loads the specified scene
    }

    public void OnLoadGame()
    {
        _loadSaveMenu.SetActive(true);
        Debug.Log("load game");
    }

    public void OnSettings()
    {
        _settingsMenu.SetActive(true);
        Debug.Log("settings");
    }

    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }

    public void CloseMenus()
    {
        if(_settingsMenu.activeInHierarchy || _loadSaveMenu.activeInHierarchy)
        {
            _settingsMenu.SetActive(false);
            _loadSaveMenu.SetActive(false);

            // For closing menus within the main menu
        }
    }

}
