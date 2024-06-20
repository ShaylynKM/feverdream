using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
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

    private void Awake()
    {
        _pauseMenu.SetActive(false);
        _quitMainMenu.SetActive(false);
        _savePanel.SetActive(false);
        _savedPanel.SetActive(false);
        _loadPanel.SetActive(false);
        _settingsMenu.SetActive(false);
    }

    public void OnPause()
    {
        Time.timeScale = 0f;

        _pauseMenu.SetActive(true);
    }

    public void OnResume()
    {
        _pauseMenu.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OnQuit()
    {
        Time.timeScale = 1f; // Bug where the game would stay paused when loading a new game after quitting to the main menu. This fixed it
        SceneManager.LoadScene("MainMenu");
    }

    public void OnMainMenu()
    {
        _quitMainMenu.SetActive(true);
        _pauseMenu.SetActive(false);
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
