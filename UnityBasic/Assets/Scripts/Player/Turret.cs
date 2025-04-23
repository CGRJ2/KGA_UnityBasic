using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private ObjectPool bulletPool;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject muzzPoint;

    void Start()
    {
        bulletPool = new ObjectPool(bulletPrefab, 10, "bulletPool");
    }

    public void Shot()
    {
        GameObject instance = 
        bulletPool.GetPoolObj(muzzPoint.transform.position, muzzPoint.transform.rotation);

        if (instance == null)
        {
            Debug.Log("장전 중!");
            return;
        }
        
        instance.SetActive(true);
    }

    public void ChargeShot(float gauze)
    {
        GameObject instance =
        bulletPool.GetPoolObj(muzzPoint.transform.position, muzzPoint.transform.rotation);

        if (instance == null)
        {
            Debug.Log("장전 중!");
            return;
        }

        instance.GetComponent<Bullet>().Charged(gauze);
        instance.SetActive(true);
    }

    void OnDestroy()
    {
        bulletPool.DestroyAll();
    }
}
