using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] protected float radius;

    public void PosAndRotSet(float angle, Transform target, float radius = -1)
    {
        float r = 0;
        if (radius == -1) r = this.radius;
        else r = radius;

        Vector3 localPos = new Vector3(Mathf.Cos(angle) * r, 0f, Mathf.Sin(angle) * r);
        transform.localPosition = localPos;
        transform.LookAt(target);
    }
}
