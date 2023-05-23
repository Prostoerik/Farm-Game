using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{
    private GameObject player;
    private GameObject inventory;
    private SpriteRenderer deskSpriteRenderer;
    private bool allowClick = false;

    public Sprite NonclickableSprite;
    public Sprite ClickableSprite;

    public GameObject elementToOpen;

    // Start is called before the first frame update
    void Start()
    {
        deskSpriteRenderer = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        elementToOpen.SetActive(false);
        //inventory = GameObject.FindWithTag("Inventory");
    }

    void OnMouseDown()
    {
        if (allowClick)
        {
            elementToOpen.SetActive(true);
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
