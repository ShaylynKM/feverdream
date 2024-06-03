using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _quitWithoutSaving;

    [SerializeField]
    private GameObject _settingsMenu;

    [SerializeField]
    private GameObject _loadSaveMenu;

    private void Awake()
    {
        _pauseMenu.SetActive(false);
        _quitWithoutSaving.SetActive(false);
        //_settingsMenu.SetActive(false);
        //_loadSaveMenu.SetActive(false);
    }

    public void OnPause()
    {
        Time.timeScale = 0f;

        _pauseMenu.SetActive(true);
    }

    public void OnResume()
    {
        Time.timeScale = 1f;

        _pauseMenu.SetActive(false);
    }

    public void OnSettings()
    {
        //_settingsMenu.SetActive(true);
        Debug.Log("settings");
    }

    public void OnQuitWithoutSaving()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnMainMenu()
    {
        _quitWithoutSaving.SetActive(true);
    }

    public void OnSaveLoad()
    {
        //_loadSaveMenu.SetActive(true);
    }

    public void CloseMenus()
    {
        if (/*_settingsMenu.activeInHierarchy || _loadSaveMenu.activeInHierarchy ||*/ _quitWithoutSaving.activeInHierarchy)
        {
            //_settingsMenu.SetActive(false);
            //_loadSaveMenu.SetActive(false);
            _quitWithoutSaving.SetActive(false);

            // For closing menus within the main menu
        }
    }


}
