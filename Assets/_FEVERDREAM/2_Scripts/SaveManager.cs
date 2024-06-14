using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
using BayatGames.SaveGameFree;
 
public class SaveManager : MonoBehaviour {
 
    public int score;
    public string savedScene; 

    [SerializeField] bool testSave = false; 
 
    void Start () {
 
        // Saving the data
        Debug.Log("I am a SaveManager");
        
        
    }

    private void Update()
    {
        if (testSave == true)
        {
            savedScene = SceneManager.GetActiveScene().name;
            SaveGame.Save<string>("savedScene", savedScene); 
        }
        
    }

        
}