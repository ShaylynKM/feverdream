using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField]
    private Button _continueButton;

    [SerializeField]
    private GameObject _warningText;

    private string _savedScene;

    private void Awake()
    {
        _settingsMenu.SetActive(false);
        _newGameMenu.SetActive(false);
        _quitGameMenu.SetActive(false);

        // Checking to see if there's save data
        SaveData data = SaveManager.OnLoadGame();
        if (data != null)
        {
            _savedScene = data.SavedScene;
            _continueButton.interactable = true; // If save data exists, enable the continue button
        }
        else
        {
            _continueButton.interactable = false; // If there is no saved data, disable the continue button
        }
    }

    public void OnNewGameButton()
    {
        _newGameMenu.SetActive(true);

        if(_savedScene != null)
        {
            _warningText.SetActive(true);
        }
    }

    public void OnNewGameStart()
    {
        SaveManager.ClearSave();
        SceneManager.LoadScene(_firstSceneIndex); // Loads the specified scene
    }

    public void OnContinue()
    {
        if (_savedScene != null)
        {
            Debug.Log("loading scene" + _savedScene);

            SceneManager.LoadScene(_savedScene); // Loads the currently saved scene
        }
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
