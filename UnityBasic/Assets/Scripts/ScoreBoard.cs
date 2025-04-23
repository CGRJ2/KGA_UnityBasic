using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_Text scoreTXT;

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GM �ν��Ͻ� ���� ���� ������ �߰� �õ���");
            return;
        }
        else
            GameManager.Instance.ScoreUpdatedEvent.AddListener(ScoreUpdate);
    }

    private void OnDisable()
    {
        GameManager.Instance.ScoreUpdatedEvent.RemoveListener(ScoreUpdate);
    }

    public void ScoreUpdate(int score)
    {
        Debug.Log(score);
        scoreTXT.text = score.ToString();
    }


}
