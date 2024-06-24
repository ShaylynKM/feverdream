using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField]
    private AudioSource _SFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (_SFX != null && !_SFX.isPlaying)
        {
            PlaySFX();
        }
    }

    public void PlaySFX()
    {
        _SFX.Play(); // For sound effects

    }
}
