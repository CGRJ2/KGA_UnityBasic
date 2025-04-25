using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowShooter : MonoBehaviour
{
    // 플레이어의 tranform을 알아야함
    // 그래서 gm에서 플레이어를 받아옴
    private GameManager gm;

    private ObjectPool arrowPool;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int count;
    [SerializeField] private GameObject spawnPoints_Fixed;
    [SerializeField] private GameObject spawnPoints_Random;
    [SerializeField] private List<Transform> spawnPoint_multieShot;

    [SerializeField] float coolTime_A;    // Single_Shot
    [SerializeField] float coolTime_B;    // Multie_Shot_Straight
    [SerializeField] float coolTime_C;    // Multie_Shot_Circle
    [SerializeField] float coolTime_D;    // Wave_Multie_Circle_Wave
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
            if (gm.scoreTime >= 1f && !isOnPattern_A)
            {
                StartCoroutine(A_Pattern());
                isOnPattern_A = true;
            }

            /*if (gm.scoreTime >= 15f && !isOnPattern_B)
            {
                StartCoroutine(B_Pattern());
                isOnPattern_B = true;
            }*/

            if (gm.scoreTime >= 6.5f && !isOnPattern_C)
            {
                StartCoroutine(C_Pattern());
                isOnPattern_C = true;
            }

            if (gm.scoreTime >= 10f && !isOnPattern_D)
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
            spawnPoints_Random.GetComponent<SpawnPoint>().PosAndRotSet(randomAngle, gm.player.transform);
            Shot(spawnPoints_Random.transform);
            yield return new WaitForSeconds(coolTime_A);
        }
        isOnPattern_A = false;
        yield break;
    }

    IEnumerator B_Pattern() // Multie_Shot_Straight
    {
        while (gm.GmState == GameState.OnGame)
        {
            //Shot();


            yield return new WaitForSeconds(coolTime_B);
        }
        isOnPattern_B = false;
        yield break;
    }

    IEnumerator C_Pattern() // Multie_Shot_Circle
    {
        while (gm.GmState == GameState.OnGame)
        {
            Transform parent = spawnPoints_Fixed.transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                Shot(parent.GetChild(i), gm.player.transform);
            }
            yield return new WaitForSeconds(coolTime_C);
        }
        isOnPattern_C = false;
        yield break;
    }

    IEnumerator D_Pattern() // Wave_Multie_Circle_Wave
    {
        while(gm.GmState == GameState.OnGame)
        {
            Transform parent = spawnPoints_Fixed.transform;
            for (int i = 0; i < parent.childCount; i++)
            {
                Shot(parent.GetChild(i), gm.player.transform);
                yield return new WaitForSeconds(0.5f);
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
        arrow.transform.position = spawnPoint.position;

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
