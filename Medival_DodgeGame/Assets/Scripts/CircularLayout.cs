using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularLayout : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private bool autoUpdate = false;

    private void Start()
    {
        ArrangeChildrenInCircle();
    }

    private void OnValidate()
    {
        if (autoUpdate)
        {
            ArrangeChildrenInCircle();
        }
    }

    public void ArrangeChildrenInCircle()
    {
        int childCount = transform.childCount;

        if (childCount == 0) return;

        float angleStep = 360f / childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);

            float angle = i * angleStep * Mathf.Deg2Rad;

            Vector3 localPos = new Vector3(
                Mathf.Cos(angle) * radius,
                0f,
                Mathf.Sin(angle) * radius
            );

            child.localPosition = localPos;

            // 자식 오브젝트가 부모를 바라보게 하기
            child.LookAt(transform.position);

            // 필요 시 y축 회전만 하고 싶다면 아래처럼 수정:
            // Vector3 lookPos = transform.position;
            // lookPos.y = child.position.y; // y 고정
            // child.LookAt(lookPos);
        }
    }
}
