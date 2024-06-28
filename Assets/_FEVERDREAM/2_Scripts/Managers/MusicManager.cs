using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField]
    private AudioSource _music;

    [SerializeField] bool playOnStart;

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
            if(_music == null)
                _music = GetComponentInChildren<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        if (_music != null && playOnStart)
        {
            PlayMusic();
        }
    }
    public void PlayMusic(AudioData audioData)
    {
        if (!audioData.isPlaying)
        {
            StopMusic();
            return;
        }
        if (audioData.audioFile == _music.clip || audioData.audioFile == null)
            return;
        _music.clip = audioData.audioFile;
        _music.loop = audioData.isLooping;
        _music.volume = audioData.volume;
        _music.priority = audioData.priority;
        PlayMusic();
    }
    public void PlayMusic()
    {
        
        if(_music)
            _music.Play(); // For bgm

    }
    public void StopMusic()
    {
        if (_music.isPlaying)
            _music.Stop();
    }
}
