using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance;

    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _quitMainMenu;

    [SerializeField]
    private GameObject _savePanel;

    [SerializeField]
    private GameObject _savedPanel;

    [SerializeField]
    private GameObject _loadPanel;

    [SerializeField]
    private GameObject _settingsMenu;

    [SerializeField]
    private Button _loadButton;

    private string _savedScene;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _pauseMenu.SetActive(false);
        _quitMainMenu.SetActive(false);
        _savePanel.SetActive(false);
        _savedPanel.SetActive(false);
        _loadPanel.SetActive(false);
        _settingsMenu.SetActive(false);

        IsPaused = false;

        // Checking to see if there's save data
        SaveData data = SaveManager.OnLoadGame();
        if(data != null)
        {
            _savedScene = data.SavedScene;
            _loadButton.interactable = true; // If save data exists, enable the load button
        }
        else
        {
            _loadButton.interactable = false; // If there is no saved data, disable the load button
        }
    }

    public void OnPause()
    {
        Time.timeScale = 0f;

        _pauseMenu.SetActive(true);

        IsPaused = true;
    }

    public void OnResume()
    {
        _pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        IsPaused = false;
    }

    public void OnMainMenu()
    {
        _quitMainMenu.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    public void OnQuitToMainMenu()
    {
        Time.timeScale = 1f; // Bug where the game would stay paused when loading a new game after quitting to the main menu. This fixed it

        IsPaused = false;

        SceneManager.LoadScene("MainMenu");
    }

    // Pressing the save button
    public void OnSave()
    {
        _savePanel.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    // Selecting "yes" to save
    public void OnYesSave()
    {
        _savedScene = SceneManager.GetActiveScene().name; // Gets our active scene
        SaveManager.OnSave(_savedScene); // Assigns active scene as the saved scene
        Debug.Log("saved scene" + _savedScene);
        _savedPanel.SetActive(true); // Opens the "saved" message
    }

    // Pressing the load button
    public void OnLoad()
    {
        _loadPanel.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    // Selecting "yes" to load
    public void OnYesLoad()
    {
        if (_savedScene != null)
        {
            Time.timeScale = 1f;
            IsPaused = false; // Unpause the game before the scene changes to avoid weird behaviour

            Debug.Log("loading scene" + _savedScene);

            SceneManager.LoadScene(_savedScene); // Loads the currently saved scene
        }
    }

    public void OnSettings()
    {
        _settingsMenu.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    public void CloseMenus()
    {
        if (_savePanel.activeInHierarchy || _savePanel.activeInHierarchy || _loadPanel.activeInHierarchy || _quitMainMenu.activeInHierarchy || _settingsMenu.activeInHierarchy)
        {
            _savePanel.SetActive(false);
            _quitMainMenu.SetActive(false);
            _savedPanel.SetActive(false);
            _loadPanel.SetActive(false);
            _settingsMenu.SetActive(false);
            _pauseMenu.SetActive(true);

            // For closing menus within the main menu
        }
    }


}
