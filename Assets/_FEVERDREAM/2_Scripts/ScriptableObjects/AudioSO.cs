using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioPairs", menuName = "ScriptableObjects/AudioPairs", order = 2)]
public class AudioSO : ScriptableObject
{
    public List<AudioPairs> MyAudioPairs = new List<AudioPairs>();
}

[System.Serializable]
public struct AudioPairs
{
    public AudioNames AudioKey;
    public AudioSource AudioValue;
}

public enum AudioNames
{
    SpookyBeepBoop, SpookierBeepBoop, SpookiestBeepBoop
}
