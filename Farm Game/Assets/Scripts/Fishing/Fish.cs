using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    public Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Wall wall = collision.GetComponent<Wall>();
        if (wall != null)
        {
            Destroy(gameObject);
        }
        Hook hook = collision.GetComponent<Hook>();
        if (hook != null)
        {
            hook.catchFish(gameObject);
        }
    }
}
