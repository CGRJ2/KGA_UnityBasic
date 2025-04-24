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
        // 지속적으로 랜덤한 위치를 돌고 있음

        if (Input.GetKeyDown(KeyCode.D))
        {
            Shoot(spawnPoints_Fixed[0].position, transform.position);
        }
    }

    // 노말 샷 (랜덤 방향 사출)
    // 멀티 샷 (고정된 12방향 동시 사출)
    // 웨이브 샷 (12방향 순차적으로 사출, 파도타기식)
    // 각각 주기가 있음. 시간초가 10초씩 경과할수록 그 주기가 짧아지는 매커니즘
    private void Shoot(Vector2 origin, Vector2 dir)
    {
        GameObject arrow = arrowPool.DisposePooledObj();
        arrow.transform.position = origin;
        arrow.transform.forward = dir;
        arrow.SetActive(true);
    }

}
