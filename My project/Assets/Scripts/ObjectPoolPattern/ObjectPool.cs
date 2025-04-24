using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool
{
    private Stack<GameObject> pool = new Stack<GameObject>();
    private GameObject poolObj; // Ǯ ������Ʈ�� ���� Ǯ �θ� ������Ʈ
    public ObjectPool(GameObject prefab, int size, string poolName = "ObjectPool")
    {
        // Ǯ ������Ʈ ���� (���� ��)
        poolObj = new GameObject(poolName);

        for (int i = 0; i < size; i++)
        {
            // Ǯ ������Ʈ�� ��� (������Ʈ)
            GameObject instance = Object.Instantiate(prefab, poolObj.transform);

            // ����Ʈ�� ���
            pool.Push(instance);

            // �ٽ� Ǯ�� ���� �� �ʿ��� ����
            Pooled_Obj PoolChild = instance.GetComponent<Pooled_Obj>();

            // Ǯ�� ���� ������ �ν��Ͻ����� Pooled_Obj ������Ʈ�� ���� �ִ��� ���� �Ǵ�.
            if (PoolChild == null)
            {
                Debug.LogError("�����Ϸ��� Pool�� Prefab�� Pooled_Obj ������Ʈ�� �����ϴ�.");
                return;
            }

            // �ٽ� Ǯ�� ���� �� �ʿ��� ����
            PoolChild.ParentPool = this;
            instance.SetActive(false);
        }
    }


    public void ReturnPooledObj(GameObject obj)
    {
        pool.Push(obj);
    }

    public GameObject DisposePooledObj()
    {
        if (pool.Count > 0)
        {
            return pool.Pop();
        }
        else
        {
            Debug.LogWarning("������ƮǮ �����.");
            return null;
        }
    }
    public void DestroyAll()
    {
        // Ǯ ����
        Object.Destroy(poolObj);
        pool = null;
    }

}
