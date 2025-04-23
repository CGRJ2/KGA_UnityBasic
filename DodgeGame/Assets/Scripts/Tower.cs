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

        LayerMask noTower = LayerMask.GetMask("Player");          // "�÷��̾�" ���̾� �ν� , Ʈ���� ����(QueryTriggerInteraction.Ignore)
        // ���� �浹�� �ִ� ���
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, lookDistance, noTower, QueryTriggerInteraction.Ignore))
        {
            GameObject target = hitInfo.collider.gameObject;

            // target���� ���� ������
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

            // ���� �������� shot
            if (shotCooling == null)
                shotCooling = StartCoroutine(Shooting());

        }
        // ���� �浹�� ���� ���
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