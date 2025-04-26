using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : Pooled_Obj
{
    float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    private void OnEnable()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.GmState == GameState.OnGame)
        {

        }
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.magnitude > 3.5f)
        {
            Debug.Log("ȭ�� ������! ��Ȱ��ȭ!");
            gameObject.SetActive(false);
        }
    }

}
