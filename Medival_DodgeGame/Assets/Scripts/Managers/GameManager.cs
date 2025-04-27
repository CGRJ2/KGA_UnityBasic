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


    // �� ��ȯ �� ������ �ʱ�ȭ �ʿ�.

    public GameState GmState { get { return gameState; } }
 
    
    // ���� �ε�� ������ ȣ��Ǵ� �Լ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� ��ȯ ��, ���� ������ ���� ����
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
        // ���ӿ��� ȿ��, ����, �г� �ҷ����� �ֱ�

        if (bestRecord < scoreTime)
        {
            // �ű�� ����!
            bestRecord = scoreTime;

            // �ű�� ����Ʈ �ֱ� => New Record!!!
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;

        // ���� ������ ������� timeScale�� ������
        if (gameState == GameState.GameOver) return;

        // �Ϲ� Pause��� PauseEvent �߻�
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