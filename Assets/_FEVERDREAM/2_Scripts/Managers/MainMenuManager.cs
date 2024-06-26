using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject _newGameMenu;

    [SerializeField]
    private GameObject _quitGameMenu;

    private void Awake()
    {
        _settingsMenu.SetActive(false);
        _newGameMenu.SetActive(false);
        _quitGameMenu.SetActive(false);
    }

    public void OnNewGameButton()
    {
        _newGameMenu.SetActive(true);
    }

    public void OnNewGameStart()
    {
        SceneManager.LoadScene(_firstSceneIndex); // Loads the specified scene
    }

    public void OnContinue()
    {
        Debug.Log("load game");
    }

    public void OnSettings()
    {
        _settingsMenu.SetActive(true);
    }

    public void OnQuitButton()
    {
        _quitGameMenu.SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }

    public void CloseMenus()
    {
        if (_settingsMenu.activeInHierarchy || _quitGameMenu.activeInHierarchy || _newGameMenu.activeInHierarchy)
        {
            _settingsMenu.SetActive(false);
            _quitGameMenu.SetActive(false);
            _newGameMenu.SetActive(false);

            // For closing menus within the main menu
        }
    }

}
