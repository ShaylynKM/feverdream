using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        _pauseMenu.SetActive(false);
        _quitMainMenu.SetActive(false);
        _savePanel.SetActive(false);
        _savedPanel.SetActive(false);
        _loadPanel.SetActive(false);
        _settingsMenu.SetActive(false);

        IsPaused = false;

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

    public void OnSave()
    {
        _savePanel.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    public void OnLoad()
    {
        _loadPanel.SetActive(true);
        _pauseMenu.SetActive(false);
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
