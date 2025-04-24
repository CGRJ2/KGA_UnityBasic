using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooled_Obj : MonoBehaviour
{
    protected ObjectPool parentPool;
    public ObjectPool ParentPool { get { return parentPool; } set { parentPool = value; } }

    private bool generated = false;

    

    private void OnDisable()
    {
        if (!generated)
        {
            generated = true;
            return;
        }

        parentPool.ReturnPooledObj(gameObject);
    }
}
