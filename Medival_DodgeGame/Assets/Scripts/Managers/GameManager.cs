using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameObject player;

    [SerializeField] private GameState gameState;
    public GameState GmState { get { return gameState; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameState = GameState.OnGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum GameState
{
    Pause, OnGame
}