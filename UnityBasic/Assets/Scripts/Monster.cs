using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.Image;

public class Monster : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject turret;
    [SerializeField] GameObject body;

    [SerializeField] int hp;
    [SerializeField] int point;
    [SerializeField] float rayDistance;
    
    [SerializeField] float rotateSpeed;
    [SerializeField] float bodyRotateSpeed;
    [SerializeField] float moveSpeed;

    [SerializeField] float damagedCoolTime;


    Vector3 rayOffset;
    
    
    GameObject target;
    Rigidbody rb;

    bool onDamaged = false;
    bool isOnChasing = false;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 originPos = turret.transform.position + rayOffset;

        // 같은 몬스터 레이어들끼리는 무시, 트리거 콜라이더 무시
        int layerMask = ~LayerMask.GetMask("Monster");
        if (Physics.Raycast(originPos, turret.transform.forward, out RaycastHit hitInfo, rayDistance, layerMask, QueryTriggerInteraction.Ignore) 
            && !isOnChasing)
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.DrawLine(originPos, hitInfo.point, Color.green);

                target = hitInfo.collider.gameObject;
                ChasingPlayer(target);
            }
            else
            {
                Debug.DrawLine(originPos, hitInfo.point, Color.red);

                target = null;
                // 둘러보기
                turret.transform.Rotate(transform.up, Time.deltaTime * rotateSpeed);
            }
        }
        else if (isOnChasing)
        {
            Debug.DrawLine(originPos, hitInfo.point, Color.green);

            ChasingPlayer(target);
        }
    }


    // 기즈모 설정 (chasing 영역 디버그용)
    private void OnDrawGizmos()
    {
        // 색 설정
        if (isOnChasing) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;

        // 출력
        SphereCollider chasingRange = GetComponent<SphereCollider>();
        if (chasingRange != null)
        {
            Gizmos.matrix = chasingRange.transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(chasingRange.center, chasingRange.radius);
        }
    }

    public void ChasingPlayer(GameObject target)
    {
        // 몸 방향 맞추고 따라오기
        Vector3 targetVec = target.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetVec);

        body.transform.rotation = Quaternion.Lerp(body.transform.rotation, targetRotation, bodyRotateSpeed * Time.deltaTime);
        turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, targetRotation, bodyRotateSpeed * Time.deltaTime);
        
        rb.velocity = body.transform.forward * moveSpeed;
    }


    public void ChasingStateChange(GameObject target)
    {
        this.target = target;
        isOnChasing = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            target = other.gameObject;
            isOnChasing = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            target = null;
            isOnChasing = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (onDamaged) return;

        onDamaged = true;
        
        hp -= damage;
        if (hp <= 0) Dead();

        StartCoroutine(DamagedCool());
    }
    IEnumerator DamagedCool()
    {
        yield return new WaitForSeconds(damagedCoolTime);
        onDamaged = false;
        yield break;
    }
    void Dead()
    {
        Destroy(gameObject);
        GameManager.Instance.GetScore(point);
    }
}
