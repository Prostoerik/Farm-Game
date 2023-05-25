using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private bool canSpawnLoot = true;
    private bool allowClick = false;
    private Player player;
    private SpriteRenderer spriteRenderer;

    public Item loot;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameManager.instance.player;
    }

    void OnMouseDown()
    {
        if (allowClick && canSpawnLoot)
        {
            SpawnLoot();
            canSpawnLoot = false;
            Invoke("ResetSpawnFlag", 10f);
        }
    }

    private void SpawnLoot()
    {
        int lootCount = Random.Range(2, 5);
        for (int i = 0; i < lootCount; i++)
        {
            Instantiate(loot, transform.position - new Vector3(Random.Range(-1f, 1f), spriteRenderer.bounds.size.y / 2, 0f), Quaternion.identity);
        }
    }

    private void ResetSpawnFlag()
    {
        canSpawnLoot = true;
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
