using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public List<DialogueLine> lines = new List<DialogueLine>();

}
