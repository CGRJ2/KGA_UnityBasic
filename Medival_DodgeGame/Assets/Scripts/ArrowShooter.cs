using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowShooter : MonoBehaviour
{
    private ObjectPool arrowPool;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int count;
    [SerializeField] private GameObject spawnPoints_Fixed;
    [SerializeField] private List<Transform> spawnPoint_multieShot;

    private void Awake()
    {
        arrowPool = new ObjectPool(arrowPrefab, count, "ArrowPooling");
    }

    void Start()
    {
        
    }

    void Update()
    {
        // ���������� ������ ��ġ�� ���� ����

        if (Input.GetKeyDown(KeyCode.D))
        {
            Transform parent = spawnPoints_Fixed.transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                Debug.Log("��");
                Shoot(parent.GetChild(i), GameManager.Instance.player.transform);
            }
        }
    }

    // �븻 �� (���� ���� ����)
    // ��Ƽ �� (������ 12���� ���� ����)
    // ���̺� �� (12���� ���������� ����, �ĵ�Ÿ���)
    // ���� �ֱⰡ ����. �ð��ʰ� 10�ʾ� ����Ҽ��� �� �ֱⰡ ª������ ��Ŀ����
    private void Shoot(Transform spawnPoint, Transform target)
    {
        // ȭ���� ��ȯ�ϸ� forward�� �����ӵ��� �̵���
        // ���⼭ ȭ���� forward�� ������������ ȭ���� ��� ���� �߻�Ǵ��� �����Ұ���

        // ������ �������� ���? => ȭ�� rotation�� �׳� spawnPoint�� rotation�� ���߸� ��. (spawnPoint ��ü�� �߽��� �ٶ󺸰� �ִ� �ɷ� ������� ����)
        GameObject arrow = arrowPool.DisposePooledObj();
        arrow.transform.position = spawnPoint.position;
        arrow.transform.rotation = spawnPoint.rotation;

        // �÷��̾ Ÿ������ ���?
        // ȭ���� rotation�� dir���� �ٲ������
        Vector3 direction = target.position - arrow.transform.position;
        arrow.transform.forward = direction;

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.up); // y�� ȸ��

        arrow.SetActive(true);
    }

}
