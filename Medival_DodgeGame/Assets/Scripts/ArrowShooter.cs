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
        // 지속적으로 랜덤한 위치를 돌고 있음

        if (Input.GetKeyDown(KeyCode.D))
        {
            Transform parent = spawnPoints_Fixed.transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                Debug.Log("슛");
                Shoot(parent.GetChild(i), GameManager.Instance.player.transform);
            }
        }
    }

    // 노말 샷 (랜덤 방향 사출)
    // 멀티 샷 (고정된 12방향 동시 사출)
    // 웨이브 샷 (12방향 순차적으로 사출, 파도타기식)
    // 각각 주기가 있음. 시간초가 10초씩 경과할수록 그 주기가 짧아지는 매커니즘
    private void Shoot(Transform spawnPoint, Transform target)
    {
        // 화살을 소환하면 forward로 일정속도를 이동함
        // 여기서 화살의 forward를 변경해줌으로 화살이 어딜 향해 발사되는지 조정할거임

        // 일정한 방향으로 쏜다? => 화살 rotation을 그냥 spawnPoint의 rotation에 맞추면 됨. (spawnPoint 자체가 중심을 바라보고 있는 걸로 만들었기 때문)
        GameObject arrow = arrowPool.DisposePooledObj();
        arrow.transform.position = spawnPoint.position;
        arrow.transform.rotation = spawnPoint.rotation;

        // 플레이어를 타겟으로 쏜다?
        // 화살의 rotation을 dir으로 바꿔줘야함
        Vector3 direction = target.position - arrow.transform.position;
        arrow.transform.forward = direction;

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.up); // y축 회전

        arrow.SetActive(true);
    }

}
