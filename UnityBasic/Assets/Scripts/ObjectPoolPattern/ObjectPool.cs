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
        // 풀 만들기 (이름으로 생성 가능)
        GameObject poolParent = new GameObject(poolName);
        poolObj = poolParent;
        for (int i = 0; i < size; i++)
        {
            GameObject instance = Object.Instantiate(prefab, poolParent.transform);
            pool.Add(instance);
            instance.gameObject.SetActive(false);

            // 다시 풀에 넣을 때 필요한 참조
            instance.GetComponent<PooledObject>().objectPool = this;
        }
    }

    public GameObject GetPoolObj(Vector3 position, Quaternion rotation)
    {
        if  (pool.Count <= 0)
        {
            Debug.Log("풀 오브젝트 모두 사용 중!");
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
        // 풀 삭제
        Object.Destroy(poolObj);
        pool = null;
    }

}
