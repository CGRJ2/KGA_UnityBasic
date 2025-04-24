using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    private ObjectPool arrowPool;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int count;
    [SerializeField] private List<Transform> spawnPoints_Fixed;
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
            Shoot(spawnPoints_Fixed[0].position, transform.position);
        }
    }

    // �븻 �� (���� ���� ����)
    // ��Ƽ �� (������ 12���� ���� ����)
    // ���̺� �� (12���� ���������� ����, �ĵ�Ÿ���)
    // ���� �ֱⰡ ����. �ð��ʰ� 10�ʾ� ����Ҽ��� �� �ֱⰡ ª������ ��Ŀ����
    private void Shoot(Vector2 origin, Vector2 dir)
    {
        GameObject arrow = arrowPool.DisposePooledObj();
        arrow.transform.position = origin;
        arrow.transform.forward = dir;
        arrow.SetActive(true);
    }

}
