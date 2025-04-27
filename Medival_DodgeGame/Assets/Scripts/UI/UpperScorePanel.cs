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
            tmp.text = gm.ScoreToString();
        }
    }

}
