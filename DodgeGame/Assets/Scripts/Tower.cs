using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerType type;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float lookDistance;
    [SerializeField] private float shotCooltime;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzPoint;

    Coroutine shotCooling;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (type == TowerType.Rotate)
        {
            RotateTower();
        }
        else if (type == TowerType.Detecting)
        {
            Detect();
        }
    }

    private void RotateTower()
    {
        transform.Rotate(transform.up, rotateSpeed * Time.deltaTime);
    }

    private void Detect()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * lookDistance, Color.red);

        LayerMask noTower = LayerMask.GetMask("Player");          // "플레이어" 레이어 인식 , 트리거 무시(QueryTriggerInteraction.Ignore)
        // 레이 충돌이 있는 경우
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, lookDistance, noTower, QueryTriggerInteraction.Ignore))
        {
            GameObject target = hitInfo.collider.gameObject;

            // target으로 방향 돌리기
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

            // 일정 간격으로 shot
            if (shotCooling == null)
                shotCooling = StartCoroutine(Shooting());

        }
        // 레이 충돌이 없는 경우
        else
        {
            RotateTower();
        }
    }

    private IEnumerator Shooting()
    {
        Shot();
        yield return new WaitForSeconds(shotCooltime);
        shotCooling = null;
        yield break;
    }

    private void Shot()
    {
        Instantiate(bulletPrefab, muzzPoint.position, transform.rotation);
    }

}
enum TowerType { Detecting, Rotate, Fixed }