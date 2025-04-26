using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooled_Obj : MonoBehaviour
{
    protected ObjectPool parentPool;
    public ObjectPool ParentPool { get { return parentPool; } set { parentPool = value; } }

    private bool isInit = false;

    private void OnEnable()
    {
        if (!isInit)
        {
            return;
        }
        OnPooledEnable();
    }

    // OnEnable 시, 구현해야할 기능이 있다면 오버라이드 해서 추가 (공통으로 추가할 기능은 여기에 추가)
    protected virtual void OnPooledEnable() {}

    private void OnDisable()
    {
        if (!isInit)
        {
            isInit = true;
            return;
        }

        parentPool.ReturnPooledObj(gameObject);
        OnPooledDisable();
    }

    // OnDisable 시, 구현해야할 기능이 있다면 오버라이드 해서 추가 (공통으로 추가할 기능은 여기에 추가)
    protected virtual void OnPooledDisable() {}

}
