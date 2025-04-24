using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joy;
    [SerializeField] private float speed;

    private Vector3 dir;
    private Rigidbody rb;
    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        GameManager.Instance.player = gameObject;
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }

    private void Update()
    {
        float x = joy.Horizontal;
        float z = joy.Vertical;
        if (x >= 0) GetComponent<SpriteRenderer>().flipX = true;
        else GetComponent<SpriteRenderer>().flipX = false;
            
        dir = new Vector3(x, 0, z);
    }
}
