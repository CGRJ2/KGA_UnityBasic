using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Pooled_Obj
{
    private void OnEnable()
    {
        if (GameManager.Instance.GmState == GameState.OnGame)
        {
            // �߻� ȿ�� ���
            // �������� ������� �̵��Ѵ�
            // �ݶ��̴��� ������ ��Ȱ��ȭ �Ѵ�
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
