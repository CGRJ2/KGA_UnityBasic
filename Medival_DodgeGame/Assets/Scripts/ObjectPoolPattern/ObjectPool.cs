using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool
{
    private Stack<GameObject> pool = new Stack<GameObject>();
    private GameObject poolObj; // 풀 오브젝트들 담을 풀 부모 오브젝트
    public ObjectPool(GameObject prefab, int size, string poolName = "ObjectPool")
    {
        // 풀 오브젝트 생성 (담을 곳)
        poolObj = new GameObject(poolName);

        for (int i = 0; i < size; i++)
        {
            // 풀 오브젝트에 담기 (오브젝트)
            GameObject instance = Object.Instantiate(prefab, poolObj.transform);

            // 리스트에 담기
            pool.Push(instance);

            // 다시 풀에 넣을 때 필요한 참조
            Pooled_Obj PoolChild = instance.GetComponent<Pooled_Obj>();

            // 풀에 넣을 프리펩 인스턴스들이 Pooled_Obj 컴포넌트를 갖고 있는지 여부 판단.
            if (PoolChild == null)
            {
                Debug.LogError("생성하려는 Pool의 Prefab에 Pooled_Obj 컴포넌트가 없습니다.");
                return;
            }

            // 다시 풀에 넣을 때 필요한 참조
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
            Debug.LogWarning("오브젝트풀 비었음.");
            return null;
        }
    }
    public void DestroyAll()
    {
        // 풀 삭제
        Object.Destroy(poolObj);
        pool = null;
    }

}
