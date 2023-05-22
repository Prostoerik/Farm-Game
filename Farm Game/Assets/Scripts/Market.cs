using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer marketSpriteRenderer;
    private bool allowClick = false;

    public Sprite NonclickableSprite;
    public Sprite ClickableSprite;

    public GameObject elementToOpen;
    public Button buttonToInvoke;

    void Start()
    {
        marketSpriteRenderer = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        elementToOpen.SetActive(false);
    }

    void OnMouseDown()
    {
        if (allowClick && !GameManager.instance.isDeskOpen && !GameManager.instance.isInventoryOpen && !GameManager.instance.isMarketOpen)
        {
            elementToOpen.SetActive(true);
            GameManager.instance.isMarketOpen = true;
            buttonToInvoke.onClick.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 3.5f)
        {
            allowClick = true;
            marketSpriteRenderer.sprite = ClickableSprite;
        }
        else
        {
            allowClick = false;
            marketSpriteRenderer.sprite = NonclickableSprite;
        }
    }
}
