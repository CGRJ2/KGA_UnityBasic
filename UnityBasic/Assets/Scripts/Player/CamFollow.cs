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
        // 1. 포대의 회전을 기준으로 offset을 회전시킴
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // 2. 부드럽게 위치 이동
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 3. 카메라가 포대가 바라보는 방향을 그대로 바라보게 설정
        Quaternion desiredRotation = Quaternion.LookRotation(target.forward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
    }
}
