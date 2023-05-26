using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Transform fence;
    public Transform door; // ������ �� ������ �����

    private Vector2 minBoundaries;
    private Vector2 maxBoundaries;
    private bool canEnter = false; // ����, �����������, ����� �� ����� �����

    private void Start()
    {
        // �������� ������� ����������� �������
        BoxCollider2D fenceCollider = fence.GetComponent<BoxCollider2D>();
        minBoundaries = fenceCollider.bounds.min;
        maxBoundaries = fenceCollider.bounds.max;
    }

    private void Update()
    {
        // �������� ����� ������� �������
        Vector3 newPosition = transform.position;

        // ���������, ��������� �� ����� ����� � ������
        if (canEnter)
        {
            Debug.Log("����� ����� � ������");
            // ���� ����� ����� � ������ � �������� ������� �����, ��������� ��� �����
            if (Input.GetKeyDown(KeyCode.E))
            {
                newPosition.x = door.position.x; // ���������� ������ �� x-���������� �����
                newPosition.y = door.position.y; // ���������� ������ �� y-���������� �����
            }
        }

        // ������������ ������� ������� ������ ������ ����������� �������
        newPosition.x = Mathf.Clamp(newPosition.x, minBoundaries.x, maxBoundaries.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBoundaries.y, maxBoundaries.y);

        // ��������� ������� �������
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ���� �������� ���������� � ������
        if (collision.transform == door)
        {
            canEnter = true; // ������������� ����, ��� ����� ����� �����
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ���������, ���� �������� � ������ ������������
        if (collision.transform == door)
        {
            canEnter = false; // ���������� ����, ��� ����� ����� �����
        }
    }
}
