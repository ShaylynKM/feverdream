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
    private bool _animationsAreEnabled;

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

        OnToggleFound();
        FindAnimatedObjects();
        TurnAnimationsOnAndOff();
    }
    void OnActiveSceneChanged(Scene previous, Scene next)
    {
        OnToggleFound();
        FindAnimatedObjects();
        TurnAnimationsOnAndOff();
    }


    private void OnToggleFound()
    {
        Toggle[] toggles = FindObjectsOfType<Toggle>(true);

        // Makes sure we find the correct toggle before proceeding
        foreach (Toggle toggle in toggles)
        {
            if (toggle.name == "AnimationsToggle")
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
                OnAnimationsToggle(!_animationsToggle.isOn);
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
        if (_animatedObjects == null)
        {
            _animatedObjects = GameObject.FindGameObjectsWithTag("AnimatedObject");
        }
    }

    private void TurnAnimationsOnAndOff()
    {
        Animator animator;

        // Get all the animated objects
        foreach(GameObject animatedObject in _animatedObjects)
        {
            animator = animatedObject.GetComponent<Animator>();
            
            if(_animationsAreEnabled == true)
            {
                animator.enabled = true; // Enable the animations if the bool is true
            }
            else if(_animationsAreEnabled == false)
            {
                animator.enabled = false; // Disable the animations if the bool is false
            }
        }
    }

    public void OnAnimationsToggle(bool isAnimated)
    {
        _animationsAreEnabled = !isAnimated;

        PlayerPrefs.SetInt("AnimationsOnOff", _animationsAreEnabled ? 1 : 0); // Saves the bool in PlayerPrefs as an Int, since PlayerPrefs doesn't natively support boolean values
    }


}
