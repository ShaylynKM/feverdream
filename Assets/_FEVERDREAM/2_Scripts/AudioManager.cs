using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor.Build;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float AudioVolume = 0.5f;

    [SerializeField]
    AudioSO _audioFiles;

    Dictionary<AudioNames, AudioSource> _audioSources = new Dictionary<AudioNames, AudioSource>();

    void Awake()
    {

        foreach (AudioPairs entry in _audioFiles.MyAudioPairs)
        {
            AudioSource source = Instantiate(entry.AudioValue); // Prefab instantiated
            _audioSources.Add(entry.AudioKey, source);

        }
    }

    void Start()
    {
        UpdateVolume(AudioVolume);

    }

    public void UpdateVolume(float volume)
    {
        this.AudioVolume = volume;
        foreach (KeyValuePair<AudioNames, AudioSource> entry in _audioSources)
        {
            entry.Value.volume = volume;
        }

    }

    public void Play(AudioNames name, bool isLoop = false)
    {
        if (_audioSources.ContainsKey(name))
        {
            _audioSources[name].loop = isLoop;
            _audioSources[name].Play();
        }
    }

    public void Stop(AudioNames name)
    {
        _audioSources[name]?.Stop();
    }
}
