using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    private static PlayerData instance;
    public static PlayerData Instance { get { if (instance == null) instance = new PlayerData(); return instance; } }
    private float maxScore { get; set; }


    // 게임 오버 시 호출
    public void Update_Player_New_Record(float score)
    {
        maxScore = score;
    }

    public float GetPlayerRecord()
    {
        return maxScore;
    }
}
