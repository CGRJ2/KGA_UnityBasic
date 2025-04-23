using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public List<GameObject> pool = new List<GameObject>();
    GameObject poolObj;
    int size;

    public ObjectPool(GameObject prefab, int size, string poolName = "ObjectPool")
    {
        // Ǯ ����� (�̸����� ���� ����)
        GameObject poolParent = new GameObject(poolName);
        poolObj = poolParent;
        for (int i = 0; i < size; i++)
        {
            GameObject instance = Object.Instantiate(prefab, poolParent.transform);
            pool.Add(instance);
            instance.gameObject.SetActive(false);

            // �ٽ� Ǯ�� ���� �� �ʿ��� ����
            instance.GetComponent<PooledObject>().objectPool = this;
        }
    }

    public GameObject GetPoolObj(Vector3 position, Quaternion rotation)
    {
        if  (pool.Count <= 0)
        {
            Debug.Log("Ǯ ������Ʈ ��� ��� ��!");
            return null;
        }

        GameObject instance = pool[pool.Count - 1];
        pool.RemoveAt(pool.Count - 1);

        instance.transform.position = position;
        instance.transform.rotation = rotation;

        return instance;
    }

    public void DestroyAll()
    {
        // Ǯ ����
        Object.Destroy(poolObj);
        pool = null;
    }

}
