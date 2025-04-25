using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowShooter : MonoBehaviour
{
    // �÷��̾��� tranform�� �˾ƾ���
    // �׷��� gm���� �÷��̾ �޾ƿ�
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


    // �븻 �� (���� ���� ����)
    // ��Ƽ �� (������ 12���� ���� ����)
    // ���̺� �� (12���� ���������� ����, �ĵ�Ÿ���)
    // ���� �ֱⰡ ����. �ð��ʰ� 10�ʾ� ����Ҽ��� �� �ֱⰡ ª������ ��Ŀ����
    #region �߻� ����
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





    
    
    // �߻�
    private void Shot(Transform spawnPoint, Transform target = null)
    {
        GameObject arrow = arrowPool.DisposePooledObj();
        arrow.transform.position = spawnPoint.position;

        // �������� �߻�
        if (target == null) 
        {
            // ������ �������� �� => ȭ�� rotation�� �׳� spawnPoint�� rotation�� ���߸� ��. (spawnPoint ��ü�� �߽��� �ٶ󺸰� �ִ� �ɷ� ������� ����)
            arrow.transform.rotation = spawnPoint.rotation;
        }
        // Ÿ���� ���� �߻�
        else
        {
            // ���⼭ ȭ���� forward�� ������������ ȭ���� ��� ���� �߻�Ǵ��� ����
            Vector3 direction = target.position - arrow.transform.position;
            arrow.transform.forward = direction;
        }
            
        arrow.SetActive(true);
    }


    
}
