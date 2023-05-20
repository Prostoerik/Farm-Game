using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hook : MonoBehaviour
{
    private int offset = 4;
    private bool catchingFish;
    public GameObject fishOnHook;
    public TextMeshProUGUI score;
    private int scoreInt;

    void Update()
    {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            if (worldPosition.y + offset < 7f && worldPosition.y + offset > 1.1f)
            {
                transform.position = new Vector3(transform.position.x, worldPosition.y + offset, 1);
                if (worldPosition.y + offset > 6.9f && catchingFish)
                {
                    catchingFish = false;
                    fishOnHook.SetActive(false);
                    addScore();
                }
            }
    }

    public void catchFish(GameObject fish)
    {
        if (!catchingFish)
        {
            SpriteRenderer sourceSpriteRenderer = fish.GetComponent<SpriteRenderer>();
            SpriteRenderer targetSpriteRenderer = fishOnHook.GetComponent<SpriteRenderer>();

            if (sourceSpriteRenderer != null && targetSpriteRenderer != null)
            {
                targetSpriteRenderer.sprite = sourceSpriteRenderer.sprite;
            }
            fishOnHook.SetActive(true);
            catchingFish = true;
            Destroy(fish);
        }
    }

    private void addScore()
    {
        scoreInt++;
        score.text = "Fishes:" + scoreInt;
    }
}
