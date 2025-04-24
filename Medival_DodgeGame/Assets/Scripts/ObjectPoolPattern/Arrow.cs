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
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.magnitude > 3.5f)
        {
            Debug.Log("화살 나갔다! 비활성화!");
            gameObject.SetActive(false);
        }
    }

}
