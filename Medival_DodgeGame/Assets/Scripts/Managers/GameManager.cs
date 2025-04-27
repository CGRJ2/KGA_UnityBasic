using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameObject player;
    private float startTime;
    public float scoreTime;

    [SerializeField] public float bestRecord { get; private set; }

    [SerializeField] private GameState gameState;

    public UnityEvent GameOverEvent;


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
        Time.timeScale = 1;
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


        if (Input.GetKey(KeyCode.Escape)) Pause();
        else if (Input.GetKey(KeyCode.R)) GameStart();

    }

    public void GameOver()
    {
        Pause();

        GameOverEvent?.Invoke();
        // 게임오버 효과, 사운드, 패널 불러오기 넣기

        if (bestRecord < scoreTime)
        {
            // 신기록 갱신!
            bestRecord = scoreTime;

            // 신기록 이펙트 넣기 => New Record!!!
        }

        
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gameState = GameState.Pause;
    }

}

public enum GameState
{
    Pause, OnGame
}