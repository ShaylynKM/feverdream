using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandWashSound : MonoBehaviour
{
    // For that one time I want the hand washing sound to keep going through scenes

    public static HandWashSound Instance;

    [SerializeField]
    private string _sceneToDestroyObject; // When this object should be destroyed 

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
    }
    private void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged; // Subscribes our scene changing method to the callback event

        
    }
    void OnActiveSceneChanged(Scene previous, Scene next)
    {
        if(SceneManager.GetActiveScene().name == _sceneToDestroyObject)
        {
            Destroy(gameObject); // destroy this object when it reaches the correct scene
        }

    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged; // Unsubscribes from the callback method when this object is destroyed
    }

    private void Update()
    {
        
    }
}
