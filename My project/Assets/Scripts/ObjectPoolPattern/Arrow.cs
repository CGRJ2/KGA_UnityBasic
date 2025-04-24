using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Pooled_Obj
{
    private void OnEnable()
    {
        if (GameManager.Instance.GmState == GameState.OnGame)
        {
            // 발사 효과 고고
            // 직선으로 등속으로 이동한다
            // 콜라이더에 닿으면 비활성화 한다
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    
}
