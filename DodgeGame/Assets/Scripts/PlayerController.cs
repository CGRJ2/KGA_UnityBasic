using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;

    private Vector3 dir;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void PlayerInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        dir = new Vector3(x, 0, z);
    }

    public void Move()
    {
        if (dir != Vector3.zero)
            rb.velocity = dir * moveSpeed;
    }
}
