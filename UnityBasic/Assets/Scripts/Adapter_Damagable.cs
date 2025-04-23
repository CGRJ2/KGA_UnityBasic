using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Adapter_Damagable : MonoBehaviour, IDamagable
{

    public UnityEvent DamageEvet;

    public void TakeDamage(int damage)
    {
        DamageEvet?.Invoke();
    }
}















