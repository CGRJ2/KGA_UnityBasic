using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Sirenix.OdinInspector;



public class ArrowShooter : MonoBehaviour
{
    // 플레이어의 tranform을 알아야함
    // 그래서 gm에서 플레이어를 받아옴
    private GameManager gm;

    private ObjectPool arrowPool;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int count;

    [FoldoutGroup("발사 패턴 설정")]
    [InfoBox("기본 설정", InfoMessageType.None)]
    [SerializeField] private float shotSpeed;
    [FoldoutGroup("발사 패턴 설정")]
    [Tooltip("스폰 포인트가 놓일 원의 반지름")]
    [SerializeField] private float radius;


    [FoldoutGroup("발사 패턴 설정/A 패턴")]
    [InfoBox("랜덤 각도에서 한 발 씩 타겟팅 발사", InfoMessageType.None)]
    [SerializeField] private GameObject spawnPoint_A;
    [FoldoutGroup("발사 패턴 설정/A 패턴")]
    [SerializeField] float startTime_A;
    [FoldoutGroup("발사 패턴 설정/A 패턴")]
    [SerializeField] float coolTime_A;    // Single_Shot

    [FoldoutGroup("발사 패턴 설정/B 패턴")]
    [InfoBox("12각도에서 동시에 타겟팅 발사", InfoMessageType.None)]
    [SerializeField] private GameObject spawnPoint_B;
    [FoldoutGroup("발사 패턴 설정/B 패턴")]
    [SerializeField] float startTime_B;
    [FoldoutGroup("발사 패턴 설정/B 패턴")]
    [SerializeField] float coolTime_B;    // Multie_Shot_Circle

    [FoldoutGroup("발사 패턴 설정/C 패턴")]
    [InfoBox("12각도에서 시간차로 타겟팅 발사", InfoMessageType.None)]
    [SerializeField] private GameObject spawnPoint_C;
    [FoldoutGroup("발사 패턴 설정/C 패턴")]
    [SerializeField] float startTime_C;
    [FoldoutGroup("발사 패턴 설정/C 패턴")]
    [SerializeField] float coolTime_C;    // Wave_Multie_Circle_Wave

    [FoldoutGroup("발사 패턴 설정/D 패턴")]
    [InfoBox("랜덤 각도에서 일렬로 직선 발사", InfoMessageType.None)]
    [SerializeField] private GameObject spawnPoint_D;
    [FoldoutGroup("발사 패턴 설정/D 패턴")]
    [SerializeField] float startTime_D;
    [FoldoutGroup("발사 패턴 설정/D 패턴")]
    [SerializeField] float coolTime_D;    // Multie_Shot_Straight

    bool isOnPattern_A = false;
    bool isOnPattern_B = false;
    bool isOnPattern_C = false;
    bool isOnPattern_D = false;

    private void Awake()
    {
        arrowPool = new ObjectPool(arrowPrefab, count, "ArrowPooling");
    }

    void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {

        if (gm.GmState == GameState.OnGame)
        {
            // 30초 지나면 A패턴 하나 추가할까?
            if (gm.scoreTime >= startTime_A && !isOnPattern_A)
            {
                StartCoroutine(A_Pattern());
                isOnPattern_A = true;
            }

            if (gm.scoreTime >= startTime_B && !isOnPattern_B)
            {
                StartCoroutine(B_Pattern());
                isOnPattern_B = true;
            }

            if (gm.scoreTime >= startTime_C && !isOnPattern_C)
            {
                StartCoroutine(C_Pattern());
                isOnPattern_C = true;
            }

            if (gm.scoreTime >= startTime_D && !isOnPattern_D)
            {
                StartCoroutine(D_Pattern());
                isOnPattern_D = true;
            }
        }
    }


    // 노말 샷 (랜덤 방향 사출)
    // 멀티 샷 (고정된 12방향 동시 사출)
    // 웨이브 샷 (12방향 순차적으로 사출, 파도타기식)
    // 각각 주기가 있음. 시간초가 10초씩 경과할수록 그 주기가 짧아지는 매커니즘
    #region 발사 패턴
    IEnumerator A_Pattern() // Single_Shot
    {
        while (gm.GmState == GameState.OnGame)
        {
            float randomAngle = UnityEngine.Random.Range(0f, 360f);
            spawnPoint_A.GetComponent<SpawnPoint>().PosAndRotSet(randomAngle, gm.player.transform);
            Shot(spawnPoint_A.transform);
            yield return new WaitForSeconds(coolTime_A);
        }
        isOnPattern_A = false;
        yield break;
    }

    IEnumerator B_Pattern() // Multie_Shot_Circle
    {
        while (gm.GmState == GameState.OnGame)
        {
            Transform parent = spawnPoint_B.transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                Shot(parent.GetChild(i), gm.player.transform);
            }
            yield return new WaitForSeconds(coolTime_B);
        }
        isOnPattern_B = false;
        yield break;
    }

    IEnumerator C_Pattern() // Wave_Multie_Circle_Wave
    {
        while(gm.GmState == GameState.OnGame)
        {
            Transform parent = spawnPoint_C.transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                Shot(parent.GetChild(i), gm.player.transform);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(coolTime_C);
        }
        isOnPattern_C = false;
        yield break;
    }
    IEnumerator D_Pattern() // Multie_Shot_Straight
    {
        while (gm.GmState == GameState.OnGame)
        {
            float randomAngle = UnityEngine.Random.Range(0f, 360f);
            spawnPoint_D.GetComponent<SpawnPoint>().PosAndRotSet(randomAngle, gm.player.transform);

            Transform parent = spawnPoint_D.transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                Shot(parent.GetChild(i));
            }

            yield return new WaitForSeconds(coolTime_D);
        }
        isOnPattern_D = false;
        yield break;
    }
    #endregion







    // 발사
    private void Shot(Transform spawnPoint, Transform target = null)
    {
        GameObject arrow = arrowPool.DisposePooledObj();
        if (arrow == null) return;
        arrow.transform.position = spawnPoint.position;
        arrow.GetComponent<Arrow>().Speed = shotSpeed;

        // 직선으로 발사
        if (target == null) 
        {
            // 일정한 방향으로 쏜 => 화살 rotation을 그냥 spawnPoint의 rotation에 맞추면 됨. (spawnPoint 자체가 중심을 바라보고 있는 걸로 만들었기 때문)
            arrow.transform.rotation = spawnPoint.rotation;
        }
        // 타겟을 향해 발사
        else
        {
            // 여기서 화살의 forward를 변경해줌으로 화살이 어딜 향해 발사되는지 조정
            Vector3 direction = target.position - arrow.transform.position;
            arrow.transform.forward = direction;
        }
            
        arrow.SetActive(true);
    }


    
}
