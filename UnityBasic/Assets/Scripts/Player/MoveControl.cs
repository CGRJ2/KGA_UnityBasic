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

    // �Է��� update����

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

    // ���� ó���� FixedUpdate����
    // ��ü ������Ʈ ��ǥ �̵�, ���� ������Ʈ�� ������ �ٲ㼭 �����ֱ�
    private void FixedUpdate()
    {
        // ������ ���� �̵����� �ٲ���

        // ��ü ������Ʈ ��ǥ �̵� (��ü ������Ʈ ���� ��ȭ x, ���� ���� ������������ �̵�)
        //Debug.DrawLine(transform.position, transform.position + body.transform.forward * 10, Color.green);

        if (z != 0)
        {
            rb.velocity = z * body.transform.forward * moveSpeed;
        }

        // ���� ������Ʈ ���� ��ȭ
        if (x != 0)
        {
            if (z >= 0)
                body.transform.Rotate(Vector3.up, x * Time.deltaTime * bodyRotateSpeed);
            else // ����
                body.transform.Rotate(Vector3.up, -x * Time.deltaTime * bodyRotateSpeed);
        }

    }

}
