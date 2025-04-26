using UnityEngine;
public enum SpawnPoint_Type { single, circular, aligned }

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private SpawnPoint_Type type;
    public SpawnPoint_Type Type { get { return type; } }

    private void Awake()
    {
        if (type == SpawnPoint_Type.circular) ArrangeChildrenInCircle();
    }


    // single spawn point set
    public void PosAndRotSet(float angle, Transform target, float radius = -1)
    {
        float r = 0;
        if (radius == -1) r = this.radius;
        else r = radius;

        Vector3 localPos = new Vector3(Mathf.Cos(angle) * r, 0f, Mathf.Sin(angle) * r);
        transform.localPosition = localPos;
        transform.LookAt(target);
    }

    // circular spawn point set
    public void ArrangeChildrenInCircle()
    {
        int childCount = transform.childCount;

        if (childCount == 0) return;

        float angleStep = -360f / childCount;

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

