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

    // OnEnable ��, �����ؾ��� ����� �ִٸ� �������̵� �ؼ� �߰� (�������� �߰��� ����� ���⿡ �߰�)
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

    // OnDisable ��, �����ؾ��� ����� �ִٸ� �������̵� �ؼ� �߰� (�������� �߰��� ����� ���⿡ �߰�)
    protected virtual void OnPooledDisable() {}

}
