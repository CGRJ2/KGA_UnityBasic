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

            // �ڽ� ������Ʈ�� �θ� �ٶ󺸰� �ϱ�
            child.LookAt(transform.position);

            // �ʿ� �� y�� ȸ���� �ϰ� �ʹٸ� �Ʒ�ó�� ����:
            // Vector3 lookPos = transform.position;
            // lookPos.y = child.position.y; // y ����
            // child.LookAt(lookPos);
        }
    }
}
