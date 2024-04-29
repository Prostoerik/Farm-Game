using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private bool allowClick = false;
    private Player player;

    public Item basicLoot;
    public Item valLoot1;
    public Item valLoot2;
    public Item valLoot3;

    public Sprite rock_1;
    public Sprite rock_2;

    private SpriteRenderer spriteRenderer;
    private bool isRockActive = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (Random.Range(0, 2) == 0) spriteRenderer.sprite = rock_1;
        else spriteRenderer.sprite = rock_2;
        player = GameManager.instance.player;
    }

    void OnMouseDown()
    {
        if (allowClick && player.inventory.toolbar.slots[GameManager.instance.selectedItemIndex].itemName == "Pickaxe" && isRockActive)
        {
            int lootCount = Random.Range(2, 5);
            for (int i = 0; i < lootCount; i++)
            {
                Vector3 spawnLocation = transform.position;

                float R = 1;
                float angle = Random.Range(0f, 2f * Mathf.PI);
                float x = Mathf.Cos(angle) * R;
                float y = Mathf.Sin(angle) * R;
                Vector3 spawnOffset = new Vector3(x, y, 0f);

                Instantiate(basicLoot, spawnLocation + spawnOffset * 0.5f, Quaternion.identity);
            }

            int randomValue = Random.Range(1, 11);
            if (randomValue == 3) Instantiate(valLoot1, transform.position, Quaternion.identity);
            if (randomValue == 6) Instantiate(valLoot2, transform.position, Quaternion.identity);
            if (randomValue == 9) Instantiate(valLoot3, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
            isRockActive = false;
            Invoke("ReturnRockActive", 60f);
        }
    }

    void ReturnRockActive()
    {
        gameObject.SetActive(true);
        isRockActive = true;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 3.5f)
        {
            allowClick = true;
        }
        else
        {
            allowClick = false;
        }
    }
}
