using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Transform fence;
    public Transform door; // Ссылка на объект двери

    private Vector2 minBoundaries;
    private Vector2 maxBoundaries;
    private bool canEnter = false; // Флаг, указывающий, может ли игрок войти

    private void Start()
    {
        // Получаем границы огороженной области
        BoxCollider2D fenceCollider = fence.GetComponent<BoxCollider2D>();
        minBoundaries = fenceCollider.bounds.min;
        maxBoundaries = fenceCollider.bounds.max;
    }

    private void Update()
    {
        // Получаем новую позицию объекта
        Vector3 newPosition = transform.position;

        // Проверяем, находится ли игрок рядом с дверью
        if (canEnter)
        {
            Debug.Log("Игрок рядом с дверью");
            // Если игрок рядом с дверью и нажимает клавишу ввода, позволяем ему войти
            if (Input.GetKeyDown(KeyCode.E))
            {
                newPosition.x = door.position.x; // Перемещаем игрока на x-координату двери
                newPosition.y = door.position.y; // Перемещаем игрока на y-координату двери
            }
        }

        // Ограничиваем позицию объекта внутри границ огороженной области
        newPosition.x = Mathf.Clamp(newPosition.x, minBoundaries.x, maxBoundaries.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBoundaries.y, maxBoundaries.y);

        // Обновляем позицию объекта
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, если коллизия происходит с дверью
        if (collision.transform == door)
        {
            canEnter = true; // Устанавливаем флаг, что игрок может войти
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Проверяем, если коллизия с дверью прекращается
        if (collision.transform == door)
        {
            canEnter = false; // Сбрасываем флаг, что игрок может войти
        }
    }
}
