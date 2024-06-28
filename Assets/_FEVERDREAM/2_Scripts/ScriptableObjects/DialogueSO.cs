using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public List<DialogueLine> lines = new List<DialogueLine>();

    public AudioData audioData;
}

[System.Serializable]
public struct AudioData
{
    public float volume;
    public int priority;
    public AudioClip audioFile;
    public bool isLooping;
    public bool isPlaying;
}
