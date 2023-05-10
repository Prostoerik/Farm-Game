using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Transform fence;

    private Vector2 minBoundaries;
    private Vector2 maxBoundaries;

    private void Start()
    {
        // Получаем границы области (fence)
        BoxCollider2D fenceCollider = fence.GetComponent<BoxCollider2D>();
        minBoundaries = fenceCollider.bounds.min;
        maxBoundaries = fenceCollider.bounds.max;
    }

    private void Update()
    {
        // Получаем новую позицию объекта
        Vector3 newPosition = transform.position;

        // Ограничиваем позицию объекта внутри границ области (fence)
        newPosition.x = Mathf.Clamp(newPosition.x, minBoundaries.x, maxBoundaries.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBoundaries.y, maxBoundaries.y);

        // Обновляем позицию объекта
        transform.position = newPosition;
    }
}
