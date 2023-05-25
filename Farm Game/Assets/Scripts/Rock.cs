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

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameManager.instance.player;
    }

    void OnMouseDown()
    {
        if (allowClick && player.inventory.toolbar.slots[GameManager.instance.selectedItemIndex].itemName == "PickAxe")
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

                Instantiate(basicLoot, spawnLocation + spawnOffset * 1.2f, Quaternion.identity);
            }

            int randomValue = Random.Range(1, 11);
            if (randomValue == 3) Instantiate(valLoot1, transform.position, Quaternion.identity);
            if (randomValue == 6) Instantiate(valLoot2, transform.position, Quaternion.identity);
            if (randomValue == 9) Instantiate(valLoot3, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
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
