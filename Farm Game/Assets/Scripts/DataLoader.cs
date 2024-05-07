using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Inventory;

public class DataLoader : MonoBehaviour             
{
    public void LoadData()
    {
        GameManager.instance.player.nickname = WebManager.userData.playerData.nickname;

        Vector3 newPosition = new Vector3(WebManager.userData.playerData.x_pos, WebManager.userData.playerData.y_pos, 0f);
        GameManager.instance.player.transform.position = newPosition;

        GameManager.instance.player.lvl = WebManager.userData.playerData.lvl;
        Player.lvlProgress = WebManager.userData.playerData.lvlProgress;
        GameManager.instance.lvlManager.levelUpdate();

        GameManager.instance.player.money = WebManager.userData.playerData.balance;
        GameManager.instance.moneyManager.moneyUpdate();

        GameManager.instance.player.id = WebManager.userData.playerData.id;
        if (WebManager.userData.inventoryData.backpackData.slots.Count != 0)
            GameManager.instance.player.inventory.backpack = WebManager.userData.inventoryData.backpackData;
        if (WebManager.userData.inventoryData.toolbarData.slots.Count != 0)
            GameManager.instance.player.inventory.toolbar = WebManager.userData.inventoryData.toolbarData;

        foreach (Slot slot in GameManager.instance.player.inventory.backpack.slots)
        {
            if (!slot.IsEmpty)
            {
                LoadSprite(ref slot.icon, slot.itemName);
            }
        }

        foreach (Slot slot in GameManager.instance.player.inventory.toolbar.slots)
        {
            if (!slot.IsEmpty)
            {
                LoadSprite(ref slot.icon, slot.itemName);
            }
        }

        //GameManager.instance.uiManager.RefreshAll();
    }

    public void LoadSprite(ref Sprite spriteToChange, string itemName)
    {
        foreach (Item item in GameManager.instance.itemManager.items)
        {
            if (item.data.itemName == itemName)
            {
                spriteToChange = item.data.icon;
                break;
            }
        }
    }


    public void SaveData()
    {
        var nickname = GameManager.instance.player.nickname;
        var lvl = GameManager.instance.player.lvl;
        var lvlProgress = Player.lvlProgress;
        var balance = GameManager.instance.player.money;
        var id = GameManager.instance.player.id;
        var backpack = GameManager.instance.player.inventory.backpack;
        var toolbar = GameManager.instance.player.inventory.toolbar;
        var x_pos = GameManager.instance.player.transform.position.x;
        var y_pos = GameManager.instance.player.transform.position.y;
        print(GameManager.instance.cropsManager.crops);
        WebManager.instance.SaveData(id, nickname, balance, lvl, lvlProgress, x_pos, y_pos, JsonUtility.ToJson(backpack), JsonUtility.ToJson(toolbar));
    }
}
