using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joy;
    [SerializeField] private float speed;

    private Vector2 dir;
    private Rigidbody2D rb;
    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }

    private void Update()
    {
        float x = joy.Horizontal;
        float y = joy.Vertical;
        if (x >= 0) GetComponent<SpriteRenderer>().flipX = true;
        else GetComponent<SpriteRenderer>().flipX = false;
            
        dir = new Vector2(x, y);
    }
}
