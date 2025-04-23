using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// 게임에는 GameManager 싱글톤 게임오브젝트가 있다
    /// 싱글톤 게임오브젝트는 최대점수를 가지고 있으며, 게임 시작시 0점 부터 시작한다.
    /// 몬스터가 탱크 방향으로 쫓아오며 탱크는 몬스터를 잡는 경우 스코어를 1점 올린다.
    /// 탱크가 몬스터에 닿는 경우 게임오버가 되며 'R' 키로 재시작 할 수 있다 (재시작은 씬을 다시 로딩하는 것으로 할 수 있다)
    /// 재시작시에도 최대점수는 손실되지 않으며 게임 중 가장 높은 점수를 보관하고 있는다.

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private int score;
    public int Score { get { return score; } set { score = value; } }

    [SerializeField] private int maxScore = 0;
    public bool isOnGame = true;
    
    
    public UnityEvent<int> ScoreUpdatedEvent = new UnityEvent<int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        if (maxScore < score) maxScore = score;
        Debug.Log("게임오버! 재시작:R");
        Time.timeScale = 0;
        isOnGame = false;
    }

    public void GameStart()
    {
        isOnGame = true;
        score = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("InGameScene");
    }


    private void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            GameStart();
        }
    }

    public void GetScore(int score)
    {
        this.score += score;
        ScoreUpdatedEvent?.Invoke(this.score);
    }


    /*public static void GenerateInstance()
    {
        if (instance == null) 
        {
            GameManager prefab = Resources.Load<GameManager>("GameManager");
            instance = Instantiate(prefab);
            DontDestroyOnLoad(instance);
        }
    }

    public static void ResetInstance()
    {
        Destroy(instance);
    }

    public static GameManager GetInstance()
    {
        return instance;
    }*/
}
