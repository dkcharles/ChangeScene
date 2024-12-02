using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Enum for example game states
[Serializable]  // serializable so it can be viewed in the inspector
public enum GameState
{
    OverGround,
    UnderGround
}

// GameManager is a persistent singleton class that manages the game
public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState CurrentGameState;  // current game state
    public bool GameRunning = false;    // is the game running?
    public float PlayerScore = 10;      // example player score variable
    public float PlayerHealth = 100;    // example player health variable

    private bool justChangedState = false;   // flag to check if the game state has just changed
    
    // TextMeshProUGUI object to display the player score
    // public TextMeshProUGUI healthText;   // TextMeshProUGUI object to display the player score
    // public TextMeshProUGUI scoreText;    // TextMeshProUGUI object to display the player health
    public AudioClip ambientMusic;      // music asset to be added to the variable in the inspector
    public AudioClip heavyRain;     // music asset to be added to the variable in the inspector

    #region Standard Unity Methods
    void Awake()
    {
        // Ensure that there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        Debug.Log("GameManager is running");    // delete this line after testing
        DontDestroyOnLoad(gameObject);          // persist between scenes   
        ChangeGameState(GameState.OverGround);
    }

    void Update()
    {
        // Check the current game state
        switch (CurrentGameState)
        {
            case GameState.OverGround:
                OverGround();
                break;
            case GameState.UnderGround:
                UnderGround();
                break;
        }
    } // end of update
    #endregion
    private void OverGround()
    {

            // load Unity scene if not already loaded
            if (!SceneManager.GetSceneByName("OverGround").isLoaded)
            {
                SceneManager.LoadScene("OverGround");
                AudioSystem.Instance.PlayMusic(ambientMusic, 0.1f);
                AudioSystem.Instance.PlayAmbient(heavyRain, 0.5f);
                Debug.Log("Game is running");
        }
            // Game just started

    }
    private void UnderGround() {

        // load Unity scene if not already loaded
        if (!SceneManager.GetSceneByName("UnderGround").isLoaded)
        {
            SceneManager.LoadScene("UnderGround");
        }
    }

    // Method to change the game state
    public void ChangeGameState(GameState newGameState)
    {
        justChangedState = true;
        CurrentGameState = newGameState;
    }
    // Public static method to get the singleton instance
}
