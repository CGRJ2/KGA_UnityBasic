using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// ���ӿ��� GameManager �̱��� ���ӿ�����Ʈ�� �ִ�
    /// �̱��� ���ӿ�����Ʈ�� �ִ������� ������ ������, ���� ���۽� 0�� ���� �����Ѵ�.
    /// ���Ͱ� ��ũ �������� �Ѿƿ��� ��ũ�� ���͸� ��� ��� ���ھ 1�� �ø���.
    /// ��ũ�� ���Ϳ� ��� ��� ���ӿ����� �Ǹ� 'R' Ű�� ����� �� �� �ִ� (������� ���� �ٽ� �ε��ϴ� ������ �� �� �ִ�)
    /// ����۽ÿ��� �ִ������� �սǵ��� ������ ���� �� ���� ���� ������ �����ϰ� �ִ´�.

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
        Debug.Log("���ӿ���! �����:R");
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
