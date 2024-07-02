using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField]
    private int _transitionTime;

    [SerializeField]
    private string _nextSceneName;

    [SerializeField]
    private GameObject _transitionScreen;

    private void Start()
    {
        if(_transitionScreen != null)
        {
            _transitionScreen.SetActive(false);
            StartCoroutine(LoadSceneAsync(_nextSceneName));
        }
        else
        {
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (_nextSceneName != "") // If the string for the next scene is not left empty
        {
            SceneManager.LoadScene(sceneName: _nextSceneName); // Loading by name in case I have to go back to certain scenes, such as in the case of a choice.
        }
        else
        {
            Debug.Log("You forgot to write the scene name, genius.");
        }
    }

    // For asynchronous loading
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Load by scene name

        sceneName = _nextSceneName;

        yield return new WaitForSeconds(_transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        _transitionScreen.SetActive(true);

        yield return null;
    }
}
