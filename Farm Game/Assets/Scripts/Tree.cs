using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    public Sprite[] treeSprites; 
    public float shakeInterval = 0.5f; 
    private float shakeTimer = 0f; 

    private int currentSpriteIndex = 0; 
    private bool allowClick = false;

    private Player player;
    public Item loot;

    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameManager.instance.player;
    }

    void Update()
    {
        shakeTimer -= Time.deltaTime; 

        if (shakeTimer <= 0)
        {
            spriteRenderer.sprite = treeSprites[currentSpriteIndex];

            currentSpriteIndex = (currentSpriteIndex + 1) % treeSprites.Length; 

            shakeTimer = shakeInterval; 
        }
    }

    void OnMouseDown()
    {
        if (allowClick && player.inventory.toolbar.slots[GameManager.instance.selectedItemIndex].itemName == "Axe")
        {
            int lootCount = Random.Range(1, 4);
            for (int i = 0; i < lootCount; i++)
            {
                Instantiate(loot, transform.position, Quaternion.identity);
            }
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
