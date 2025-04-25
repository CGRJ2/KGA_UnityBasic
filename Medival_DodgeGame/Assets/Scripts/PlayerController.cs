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
        float x = 0;
        float z = 0;

        if (joy.Horizontal != 0) // 조이스틱이 우선순위
            x = joy.Horizontal;
        else if (Input.GetAxis("Horizontal") != 0)
            x = Input.GetAxis("Horizontal");

        if (joy.Vertical != 0) 
            z = joy.Vertical;
        else if (Input.GetAxis("Vertical") != 0)
            z = Input.GetAxis("Vertical");
        
        dir = new Vector3(x, 0, z);
        if (dir == Vector3.zero) return;

        if (x >= 0) GetComponent<SpriteRenderer>().flipX = true;
        else GetComponent<SpriteRenderer>().flipX = false;
    }
}
