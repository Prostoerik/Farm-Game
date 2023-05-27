using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            Item item = GetComponent<Item>();

            if(item != null)
            {
                if (GameManager.instance.isBackpackCapable)
                {
                    player.inventory.Add("Backpack", item);
                    Destroy(this.gameObject);
                    GameManager.instance.uiManager.RefreshAll();
                    return;
                }
                if (GameManager.instance.isToolbarCapable)
                {
                    player.inventory.Add("Toolbar", item);
                    Destroy(this.gameObject);
                    GameManager.instance.uiManager.RefreshAll();
                }
            }
        }
    }
}
