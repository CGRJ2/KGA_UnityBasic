using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameObject player;
    private float startTime;
    public float scoreTime;

    [SerializeField] public float bestRecord { get; private set; }

    [SerializeField] private GameState gameState;

    [HideInInspector] public UnityEvent GameStartEvent = new UnityEvent();
    [HideInInspector] public UnityEvent GameOverEvent = new UnityEvent();
    [HideInInspector] public UnityEvent PauseEvent = new UnityEvent();


    // 씬 전환 시 리스너 초기화 필요.

    public GameState GmState { get { return gameState; } }
 
    
    // 씬이 로드될 때마다 호출되는 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 전환 시, 기존 리스너 전부 해제
        GameStartEvent.RemoveAllListeners();
        GameOverEvent.RemoveAllListeners();
        PauseEvent.RemoveAllListeners();
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        GameStart();
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
    void GameStart()
    {
        Time.timeScale = 1;
        gameState = GameState.OnGame;
        startTime = Time.time;
        GameStartEvent?.Invoke();
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
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

        // 게임 오버용 정지라면 timeScale만 멈춰줘
        if (gameState == GameState.GameOver) return;

        // 일반 Pause라면 PauseEvent 발생
        else
        {
            PauseEvent?.Invoke();
            gameState = GameState.Pause;
        }
    }

}

public enum GameState
{
    Pause, OnGame, GameOver
}