using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    public Sprite[] treeSprites;
    public float shakeInterval = 0.5f;

    private int currentSpriteIndex = 0;
    private bool allowClick = false;

    private Player player;

    public Item destroyLoot;
    public Item mainLoot;

    private SpriteRenderer spriteRenderer;

    private bool isTreeActive = true;
    private bool canSpawnLoot = true;
    public float delayBetweenSprites = 0.01f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameManager.instance.player;
        currentSpriteIndex = Random.Range(0, 4);
    }

    //void Update()
    //{
    //    shakeTimer -= Time.deltaTime;

    //    if (shakeTimer <= 0)
    //    {
    //        spriteRenderer.sprite = treeSprites[currentSpriteIndex];

    //        currentSpriteIndex = (currentSpriteIndex + 1) % treeSprites.Length;

    //        shakeTimer = shakeInterval;
    //    }
    //}

    void OnMouseDown()
    {
        if (allowClick && isTreeActive && canSpawnLoot)
        {
            canSpawnLoot = false;
            StartCoroutine(ChangeSprites());
            SpawnLoot();
            Invoke("ResetSpawnFlag", 10f); 
        }

        if (allowClick && player.inventory.toolbar.slots[GameManager.instance.selectedItemIndex].itemName == "Axe" && isTreeActive)
        {
            int destroyLootCount = Random.Range(2, 5);
            for (int i = 0; i < destroyLootCount; i++)
            {
                Vector3 spawnLocation = transform.position;

                float R = 1;
                float angle = Random.Range(0f, 2f * Mathf.PI);
                float x = Mathf.Cos(angle) * R;
                float y = Mathf.Sin(angle) * R;
                Vector3 spawnOffset = new Vector3(x, y, 0f);

                Instantiate(destroyLoot, spawnLocation + spawnOffset * 0.5f, Quaternion.identity);
            }

            gameObject.SetActive(false); 
            isTreeActive = false;
            Invoke("ReturnTreeActive", 60f); 
        }
    }

    private System.Collections.IEnumerator ChangeSprites()
    {
        for (int i = 0; i < treeSprites.Length; i++)
        {
            currentSpriteIndex = i;
            spriteRenderer.sprite = treeSprites[currentSpriteIndex];
            yield return new WaitForSeconds(delayBetweenSprites);
        }
    }

    private void SpawnLoot()
    {
        int mainLootCount = Random.Range(2, 5);
        for (int i = 0; i < mainLootCount; i++)
        {
            Instantiate(mainLoot, transform.position - new Vector3(Random.Range(-1f, 1f), spriteRenderer.bounds.size.y / 2, 0f), Quaternion.identity);
        }
    }

    private void ResetSpawnFlag()
    {
        canSpawnLoot = true;
    }

    void ReturnTreeActive()
    {
        gameObject.SetActive(true); 
        isTreeActive = true;
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
