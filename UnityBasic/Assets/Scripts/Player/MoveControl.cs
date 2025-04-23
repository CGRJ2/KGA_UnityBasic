using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MoveControl : MonoBehaviour
{
    public float moveSpeed;
    public float bodyRotateSpeed;
    public GameObject body;
    float x;
    float z;
    Rigidbody rb;

    [SerializeField] FixedJoystick joy;

    // 입력은 update에서

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //x = Input.GetAxis("Horizontal");
        //z = Input.GetAxis("Vertical");
        x = joy.Horizontal;
        z = joy.Vertical;
    }

    // 물리 처리는 FixedUpdate에서
    // 전체 오브젝트 좌표 이동, 몸통 오브젝트는 각도만 바꿔서 보여주기
    private void FixedUpdate()
    {
        // 리지드 물리 이동으로 바꾸자

        // 전체 오브젝트 좌표 이동 (전체 오브젝트 각도 변화 x, 몸통 기준 직진방향으로 이동)
        //Debug.DrawLine(transform.position, transform.position + body.transform.forward * 10, Color.green);

        if (z != 0)
        {
            rb.velocity = z * body.transform.forward * moveSpeed;
        }

        // 몸통 오브젝트 각도 변화
        if (x != 0)
        {
            if (z >= 0)
                body.transform.Rotate(Vector3.up, x * Time.deltaTime * bodyRotateSpeed);
            else // 후진
                body.transform.Rotate(Vector3.up, -x * Time.deltaTime * bodyRotateSpeed);
        }

    }

}
