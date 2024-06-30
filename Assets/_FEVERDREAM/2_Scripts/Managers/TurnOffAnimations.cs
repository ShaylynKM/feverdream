using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnOffAnimations : MonoBehaviour
{
    public static TurnOffAnimations Instance;

    [SerializeField]
    private Toggle _animationsToggle;

    [SerializeField]
    private bool _animationsAreEnabled = true;

    private GameObject[] _animatedObjects;

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

        _animationsAreEnabled = PlayerPrefs.GetInt("AnimationsEnabled", 1) == 1;

        OnToggleFound();
        FindAnimatedObjects();
        TurnAnimationsOnAndOff();
    }
    void OnActiveSceneChanged(Scene previous, Scene next)
    {
        _animatedObjects = null; // Clearing the array in order to find new objects
        OnToggleFound();
        FindAnimatedObjects();
        TurnAnimationsOnAndOff();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged; // Unsubscribes from the callback method when this object is destroyed
    }

    private void OnToggleFound()
    {
        Toggle[] toggles = FindObjectsOfType<Toggle>(true);

        // Makes sure we find the correct toggle before proceeding
        foreach (Toggle toggle in toggles)
        {
            if (toggle?.name == "AnimationsToggle")
            {
                _animationsToggle = toggle;
                break;
            }
        }

        if (_animationsToggle != null)
        {
            // Assigns the listener for the value changed
            _animationsToggle.onValueChanged.AddListener(delegate
            {
                OnAnimationsToggle(_animationsToggle.isOn);
            });

            _animationsToggle.isOn = _animationsAreEnabled; // Sets the toggle in the scene to on or off based on the current state
        }
        else
        {
            Debug.Log("Animation toggle not found");
        }
    }

    private void FindAnimatedObjects()
    {
        _animatedObjects = GameObject.FindGameObjectsWithTag("AnimatedObject");

        if(_animatedObjects == null)
        {
            Debug.Log("Did not find any animated objects");
        }
    }


    private void TurnAnimationsOnAndOff()
    {
        if (_animatedObjects == null)
        {
            Debug.Log("No animated objects in array.");
            return;
        }

        Animator animator;

        // Get all the animated objects
        foreach (GameObject animatedObject in _animatedObjects)
        {
            animator = animatedObject.GetComponent<Animator>();

            if (animatedObject == null)
            {
                Debug.Log("The animated object is null.");
                continue;
            }

            if (animator == null)
            {
                Debug.Log($"No Animator found on {animatedObject.name}");
            }

            if (_animationsAreEnabled == true)
            {
                animator.enabled = true; // Enable the animations if the bool is true
            }
            else
            {
                animator.enabled = false; // Disable the animations if the bool is false
            }
        }

    }

    public void OnAnimationsToggle(bool isAnimated)
    {
        _animationsAreEnabled = isAnimated;

        PlayerPrefs.SetInt("AnimationsEnabled", _animationsAreEnabled ? 1 : 0); // Saves the bool in PlayerPrefs as an Int, since PlayerPrefs doesn't natively support boolean values

        TurnAnimationsOnAndOff();
    }
}