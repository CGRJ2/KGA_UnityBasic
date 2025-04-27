using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_GameOver : Panel
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject newRecord_Obj;
    GameManager gm;

    private void Awake()
    {
        gm = GameManager.Instance;
    }

    public void GameOverPanelActive(bool isNewRecorded)
    {
        OpenPanel();
        scoreText.text = gm.ScoreToString();

        // �ű���̶��? �ű�� ��������Ʈ & ȿ�� ���⿡
        if (isNewRecorded)
        {
            newRecord_Obj?.SetActive(true);
        }
        else
        {
            newRecord_Obj?.SetActive(false);
        }
    }
}
