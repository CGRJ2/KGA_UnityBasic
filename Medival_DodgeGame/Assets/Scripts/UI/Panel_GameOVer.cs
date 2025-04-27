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

        // 신기록이라면? 신기록 스프라이트 & 효과 여기에
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
