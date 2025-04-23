using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0,0,0);
    [SerializeField] float smoothSpeed = 10f;


    void Update()
    {
        // 1. ������ ȸ���� �������� offset�� ȸ����Ŵ
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // 2. �ε巴�� ��ġ �̵�
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 3. ī�޶� ���밡 �ٶ󺸴� ������ �״�� �ٶ󺸰� ����
        Quaternion desiredRotation = Quaternion.LookRotation(target.forward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
    }
}
