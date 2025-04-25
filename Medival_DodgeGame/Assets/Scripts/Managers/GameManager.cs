using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameObject player;
    private float startTime;
    public float scoreTime;

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
        GameStart();
    }

    void GameStart()
    {
        gameState = GameState.OnGame;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.OnGame)
        {
            scoreTime = Time.time - startTime;
        }
    }
}

public enum GameState
{
    Pause, OnGame
}