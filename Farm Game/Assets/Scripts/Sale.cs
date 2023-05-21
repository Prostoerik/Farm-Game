using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class Sale : MonoBehaviour
{
    List<Image> productImages = new List<Image>();
    List<TextMeshProUGUI> productPrices = new List<TextMeshProUGUI>();

    Dictionary<string, int> inventoryItems = new Dictionary<string, int>();
    List<string> inventoryItemsNames = new List<string>();
    List<List<itemLoc>> inventoryItemsLocs = new List<List<itemLoc>>();

    List<string> itemsToSellNames;

    public GameObject panel;
    public GameObject saleSlot;

    public List<Item> itemsToSell;

    public Player player;
    public Sprite empty;

    private class itemLoc
    {
        string inv;
        int index;
        int count;

        public itemLoc(string inv, int index, int count)
        {
            this.inv = inv;
            this.index = index;
            this.count = count;
        }
    }

    void Start()
    {
        //foreach (Item item in itemsToSell)
        //{
        //    itemsToSellNames.Add(item.data.itemName);
        //}

        //refreshItemsForSale();
    }

    public void refreshItemsForSale()
    {
        inventoryItems.Clear();
        inventoryItemsNames.Clear();
        inventoryItemsLocs.Clear();
        for (int i = 0; i < 27; i++)
        {
            if (!inventoryItems.ContainsKey(player.inventory.backpack.slots[i].itemName))
            {
                inventoryItems.Add(player.inventory.backpack.slots[i].itemName, player.inventory.backpack.slots[i].count);
                inventoryItemsNames.Add(player.inventory.backpack.slots[i].itemName);
                inventoryItemsLocs.Add(new List<itemLoc>() { new itemLoc("backpack", i, player.inventory.backpack.slots[i].count) });
            }
            else
            {
                inventoryItems[player.inventory.backpack.slots[i].itemName] += player.inventory.backpack.slots[i].count;
                inventoryItemsLocs[inventoryItemsNames.IndexOf(player.inventory.backpack.slots[i].itemName)].Add(new itemLoc("backpack", i, player.inventory.backpack.slots[i].count));
            }
        }
        for (int i = 0; i < 9; i++)
        {
            if (!inventoryItems.ContainsKey(player.inventory.toolbar.slots[i].itemName))
            {
                inventoryItems.Add(player.inventory.toolbar.slots[i].itemName, player.inventory.toolbar.slots[i].count);
                inventoryItemsNames.Add(player.inventory.toolbar.slots[i].itemName);
                inventoryItemsLocs.Add(new List<itemLoc>() { new itemLoc("toolbar", i, player.inventory.toolbar.slots[i].count) } );
            }
            else
            {
                inventoryItems[player.inventory.toolbar.slots[i].itemName] += player.inventory.toolbar.slots[i].count;
                inventoryItemsLocs[inventoryItemsNames.IndexOf(player.inventory.toolbar.slots[i].itemName)].Add(new itemLoc("toolbar", i, player.inventory.toolbar.slots[i].count));
            }
        }

        productImages.Clear();
        productPrices.Clear();

        int index = 0;
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            GameObject instantiatedPrefab = Instantiate(saleSlot);
            instantiatedPrefab.transform.SetParent(panel.transform, false);
            instantiatedPrefab.GetComponentInChildren<Text>().text = index.ToString();
            index++;
        }

        productImages = GetComponentsInChildren<Image>().ToList();
        productPrices = GetComponentsInChildren<TextMeshProUGUI>().ToList();

        for (int i = 1, j = 0; i < inventoryItems.Count; i += 2, j++)
        {
            productImages[i].sprite = itemsToSell[itemsToSellNames.IndexOf(inventoryItemsNames[j])].data.icon;
        }

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            productPrices[i].text = itemsToSell[itemsToSellNames.IndexOf(inventoryItemsNames[i])].data.sellPrice.ToString();
        }
    }
}
