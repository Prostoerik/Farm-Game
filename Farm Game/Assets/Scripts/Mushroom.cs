using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private bool allowClick = false;
    private bool isMushroomActive = true;
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
        if (allowClick && isMushroomActive)
        {
            Vector3 spawnLocation = transform.position;

            float R = 1;
            float angle = Random.Range(0f, 2f * Mathf.PI);
            float x = Mathf.Cos(angle) * R;
            float y = Mathf.Sin(angle) * R;
            Vector3 spawnOffset = new Vector3(x, y, 0f);

            Instantiate(loot, spawnLocation + spawnOffset * 0.5f, Quaternion.identity);
            gameObject.SetActive(false);
            isMushroomActive = false;
            Invoke("ReturnMushroomActive", 10f);
        }
    }

    void ReturnMushroomActive()
    {
        gameObject.SetActive(true);
        isMushroomActive = true;
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
