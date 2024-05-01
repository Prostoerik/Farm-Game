using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Inventory;
using static UnityEditor.Progress;

public class DataLoader : MonoBehaviour             
{
    public void LoadData()
    {
        GameManager.instance.player.nickname = WebManager.userData.playerData.nickname;

        GameManager.instance.player.lvl = WebManager.userData.playerData.lvl;
        Player.lvlProgress = WebManager.userData.playerData.lvlProgress;
        GameManager.instance.lvlManager.levelUpdate();

        GameManager.instance.player.money = WebManager.userData.playerData.balance;
        GameManager.instance.moneyManager.moneyUpdate();

        GameManager.instance.player.id = WebManager.userData.playerData.id;
        print(WebManager.userData.inventoryData.backpackData.slots[0].itemName);
        GameManager.instance.player.inventory.backpack = WebManager.userData.inventoryData.backpackData;
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
            // Проверяем, соответствует ли название элемента искомому названию
            if (item.data.itemName == itemName)
            {
                // Если соответствует, выводим этот элемент
                spriteToChange = item.data.icon;
                break;
            }
        }
        // Загружаем префаб по его имени
        //GameObject prefab = Resources.Load<GameObject>(prefabName + ".prefab");
        //print("Assets/Prefabs/" + prefabName + ".prefab");

        //if (prefab == null)
        //{
        //    Debug.LogError("Prefab with name " + prefabName + " not found!");
        //    return;
        //}

        //// Получаем спрайт из префаба
        //Sprite sprite = prefab.GetComponent<SpriteRenderer>().sprite;

        //if (sprite != null)
        //    spriteToChange = sprite;

        //DestroyImmediate(prefab);
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
        print(JsonUtility.ToJson(backpack));
        print(JsonUtility.ToJson(toolbar));
        WebManager.instance.SaveData(id, nickname, balance, lvl, lvlProgress, JsonUtility.ToJson(backpack), JsonUtility.ToJson(toolbar));
    }
}
