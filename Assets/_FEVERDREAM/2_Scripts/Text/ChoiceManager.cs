using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField]
    private string _choiceScene;

    // OnClick event for this object's button
    public void SelectChoice()
    {
        SceneManager.LoadScene(sceneName: _choiceScene); // Loads the scene name referenced on this object
    }
}
