using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] float radius;
    public void PosAndRotSet(float angle, Transform target)
    {
        Vector3 localPos = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
        transform.localPosition = localPos;
        transform.LookAt(target);

    }
}
