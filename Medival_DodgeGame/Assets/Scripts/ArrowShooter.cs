using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Sirenix.OdinInspector;



public class ArrowShooter : MonoBehaviour
{
    // �÷��̾��� tranform�� �˾ƾ���
    // �׷��� gm���� �÷��̾ �޾ƿ�
    private GameManager gm;

    private ObjectPool arrowPool;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int count;

    [FoldoutGroup("�߻� ���� ����")]
    [InfoBox("�⺻ ����", InfoMessageType.None)]
    [SerializeField] private float shotSpeed;
    [FoldoutGroup("�߻� ���� ����")]
    [Tooltip("���� ����Ʈ�� ���� ���� ������")]
    [SerializeField] private float radius;


    [FoldoutGroup("�߻� ���� ����/A ����")]
    [InfoBox("���� �������� �� �� �� Ÿ���� �߻�", InfoMessageType.None)]
    [SerializeField] private GameObject spawnPoint_A;
    [FoldoutGroup("�߻� ���� ����/A ����")]
    [SerializeField] float startTime_A;
    [FoldoutGroup("�߻� ���� ����/A ����")]
    [SerializeField] float coolTime_A;    // Single_Shot

    [FoldoutGroup("�߻� ���� ����/B ����")]
    [InfoBox("12�������� ���ÿ� Ÿ���� �߻�", InfoMessageType.None)]
    [SerializeField] private GameObject spawnPoint_B;
    [FoldoutGroup("�߻� ���� ����/B ����")]
    [SerializeField] float startTime_B;
    [FoldoutGroup("�߻� ���� ����/B ����")]
    [SerializeField] float coolTime_B;    // Multie_Shot_Circle

    [FoldoutGroup("�߻� ���� ����/C ����")]
    [InfoBox("12�������� �ð����� Ÿ���� �߻�", InfoMessageType.None)]
    [SerializeField] private GameObject spawnPoint_C;
    [FoldoutGroup("�߻� ���� ����/C ����")]
    [SerializeField] float startTime_C;
    [FoldoutGroup("�߻� ���� ����/C ����")]
    [SerializeField] float coolTime_C;    // Wave_Multie_Circle_Wave

    [FoldoutGroup("�߻� ���� ����/D ����")]
    [InfoBox("���� �������� �Ϸķ� ���� �߻�", InfoMessageType.None)]
    [SerializeField] private GameObject spawnPoint_D;
    [FoldoutGroup("�߻� ���� ����/D ����")]
    [SerializeField] float startTime_D;
    [FoldoutGroup("�߻� ���� ����/D ����")]
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
            // 30�� ������ A���� �ϳ� �߰��ұ�?
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







    // �߻�
    private void Shot(Transform spawnPoint, Transform target = null)
    {
        GameObject arrow = arrowPool.DisposePooledObj();
        if (arrow == null) return;
        arrow.transform.position = spawnPoint.position;
        arrow.GetComponent<Arrow>().Speed = shotSpeed;

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
