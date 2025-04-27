using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpperScorePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.GmState == GameState.OnGame)
        {
            float time = gm.scoreTime;
            int seconds = Mathf.FloorToInt(time);           // 초
            int milliseconds = Mathf.FloorToInt((time * 100) % 100);  // 밀리초 (1/100초 단위)
            string score = string.Format("{0:00}:{1:00}", seconds, milliseconds);

            tmp.text = score;
        }
    }

}
