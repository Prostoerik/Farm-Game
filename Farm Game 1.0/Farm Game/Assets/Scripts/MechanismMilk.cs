using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismMilk : MonoBehaviour
{
    public GameObject milkPrefab; // Ссылка на префаб молока
    public Transform spawnObjectTransform; // Ссылка на трансформ объекта, возле которого будет спавниться молоко
    public Sprite sprite;
    public float spawnInterval = 10f; // Интервал между спаунами молока
    public float spawnDistance = 2f; // Расстояние, на котором молоко спавнится от коровы
    private float timer; // Таймер для отслеживания интервала спаунов молока

    private void Start()
    {
        timer = spawnInterval; // Устанавливаем таймер в начальное значение интервала
    }

    private void Update()
    {
        // Уменьшаем таймер
        timer -= Time.deltaTime;

        // Проверяем, если таймер достиг нуля и игрок рядом с загоном
        if (timer <= 0f && IsPlayerNearby())
        {
            SpawnMilk(); // Спауним молоко
            timer = spawnInterval; // Сбрасываем таймер
        }
    }

    private bool IsPlayerNearby()
    {
        // Получаем позицию игрока
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Вычисляем расстояние между коровой и игроком
        float distance = Vector3.Distance(spawnObjectTransform.position, playerPosition);

        // Возвращаем true, если игрок находится рядом с загоном
        return distance <= spawnDistance;
    }

    private void SpawnMilk()
    {
        // Выбираем случайную позицию относительно указанного объекта
        float randomX = spawnDistance;
        float randomY = Random.Range(-spawnDistance, spawnDistance);

        // Создаем экземпляр молока возле коровы
        Vector3 spawnPosition = spawnObjectTransform.position + new Vector3(randomX, randomY, 0f);
        GameObject newMilk = Instantiate(milkPrefab, spawnPosition, Quaternion.identity);

        // Устанавливаем спрайт для созданного объекта молока
        SpriteRenderer spriteRenderer = newMilk.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}
