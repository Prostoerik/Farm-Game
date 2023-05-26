using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismMilk : MonoBehaviour
{
    public GameObject milkPrefab; // ������ �� ������ ������
    public Transform spawnObjectTransform; // ������ �� ��������� �������, ����� �������� ����� ���������� ������
    public Sprite sprite;
    public float spawnInterval = 10f; // �������� ����� �������� ������
    public float spawnDistance = 2f; // ����������, �� ������� ������ ��������� �� ������
    private float timer; // ������ ��� ������������ ��������� ������� ������

    private void Start()
    {
        timer = spawnInterval; // ������������� ������ � ��������� �������� ���������
    }

    private void Update()
    {
        // ��������� ������
        timer -= Time.deltaTime;

        // ���������, ���� ������ ������ ���� � ����� ����� � �������
        if (timer <= 0f && IsPlayerNearby())
        {
            SpawnMilk(); // ������� ������
            timer = spawnInterval; // ���������� ������
        }
    }

    private bool IsPlayerNearby()
    {
        // �������� ������� ������
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // ��������� ���������� ����� ������� � �������
        float distance = Vector3.Distance(spawnObjectTransform.position, playerPosition);

        // ���������� true, ���� ����� ��������� ����� � �������
        return distance <= spawnDistance;
    }

    private void SpawnMilk()
    {
        // �������� ��������� ������� ������������ ���������� �������
        float randomX = spawnDistance;
        float randomY = Random.Range(-spawnDistance, spawnDistance);

        // ������� ��������� ������ ����� ������
        Vector3 spawnPosition = spawnObjectTransform.position + new Vector3(randomX, randomY, 0f);
        GameObject newMilk = Instantiate(milkPrefab, spawnPosition, Quaternion.identity);

        // ������������� ������ ��� ���������� ������� ������
        SpriteRenderer spriteRenderer = newMilk.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}
