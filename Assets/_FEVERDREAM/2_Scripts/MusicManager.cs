using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField]
    private AudioSource _music;

    private void Awake()
    {
        if (Instance == null)
        {
            // Set this as the instance
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            // Destroy the old music manager if I explicitly placed a new one
            Destroy(Instance.gameObject);

            // Set this as the new instance
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        if (_music != null && !_music.isPlaying)
        {
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        _music.Play(); // For bgm

    }
}
