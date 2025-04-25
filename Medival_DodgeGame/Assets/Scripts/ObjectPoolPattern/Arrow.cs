using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : Pooled_Obj
{
    [SerializeField] float speed;


    private void OnEnable()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.GmState == GameState.OnGame)
        {

        }
    }

    void Start()
    {
        
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
