using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    public Vector2 movement;
    public bool MoveLine;

    public float amplitude = 0.5f; // ��������� ��������
    public float frequency = 1f; // ������� ��������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (MoveLine)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            float time = Time.time; // �������� ������� �����
            float offset = Random.Range(0.02f, 0.3f); 
            float sinValueX = Mathf.Sin(time); // �������� ������ ��� ��� X
            float cosValueY = Mathf.Cos(time + offset); // �������� �������� ��� ��� Y

            Vector2 movement_to = new Vector2(1, cosValueY) * 0.1f + movement; // �������� �������� ������ � �������� �� ������ ��������

            rb.MovePosition(rb.position + movement_to * moveSpeed * Time.fixedDeltaTime);
        }
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
