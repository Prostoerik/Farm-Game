using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer deskSpriteRenderer;
    private bool allowClick = false;

    public Sprite NonclickableSprite;
    public Sprite ClickableSprite;

    public GameObject elementToOpen;

    void Start()
    {
        deskSpriteRenderer = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        elementToOpen.SetActive(false);
    }

    void OnMouseDown()
    {
        if (allowClick && !GameManager.instance.isMarketOpen && !GameManager.instance.isInventoryOpen && !GameManager.instance.isDeskOpen)
        {
            elementToOpen.SetActive(true);
            GameManager.instance.isDeskOpen = true;
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 3.5f)
        {
            allowClick = true;
            deskSpriteRenderer.sprite = ClickableSprite;
        }
        else
        {
            allowClick = false;
            deskSpriteRenderer.sprite = NonclickableSprite;
        }
    }
}
