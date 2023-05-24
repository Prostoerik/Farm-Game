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

    private int selectedIndex = -1;

    List<string> itemsToSellNames = new List<string>();

    public GameObject panel;
    public GameObject saleSlot;

    public List<Item> itemsToSell;

    public Player player;
    public Sprite empty;
    public Button sellButton;

    public TextMeshProUGUI selectedItem;
    public TextMeshProUGUI balance;

    private class itemLoc
    {
        public string inv;
        public int index;
        public int count;

        public itemLoc(string inv, int index, int count)
        {
            this.inv = inv;
            this.index = index;
            this.count = count;
        }
    }

    void Start()
    {
        foreach (Item item in itemsToSell)
        {
            itemsToSellNames.Add(item.data.itemName);
        }

        sellButton.onClick.AddListener(delegate { sellProduct(); });

        refreshItemsForSale();
    }

    public void refreshItemsForSale()
    {
        foreach (Transform child in panel.transform)
        {
            GameObject.Destroy(child.gameObject, 0);
        }

        selectedIndex = -1;

        inventoryItems.Clear();
        inventoryItemsNames.Clear();
        inventoryItemsLocs.Clear();
        for (int i = 0; i < 27; i++)
        {
            if (!player.inventory.backpack.slots[i].IsEmpty && itemsToSellNames.Contains(player.inventory.backpack.slots[i].itemName))
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
        }
        for (int i = 0; i < 9; i++)
        {
            if (!player.inventory.toolbar.slots[i].IsEmpty && itemsToSellNames.Contains(player.inventory.toolbar.slots[i].itemName))
            {
                if (!inventoryItems.ContainsKey(player.inventory.toolbar.slots[i].itemName))
                {
                    inventoryItems.Add(player.inventory.toolbar.slots[i].itemName, player.inventory.toolbar.slots[i].count);
                    inventoryItemsNames.Add(player.inventory.toolbar.slots[i].itemName);
                    inventoryItemsLocs.Add(new List<itemLoc>() { new itemLoc("toolbar", i, player.inventory.toolbar.slots[i].count) });
                }
                else
                {
                    inventoryItems[player.inventory.toolbar.slots[i].itemName] += player.inventory.toolbar.slots[i].count;
                    inventoryItemsLocs[inventoryItemsNames.IndexOf(player.inventory.toolbar.slots[i].itemName)].Add(new itemLoc("toolbar", i, player.inventory.toolbar.slots[i].count));
                }
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

        StartCoroutine(GetProductImagesNextFrame());
    }

    private IEnumerator GetProductImagesNextFrame()
    {
        yield return null; // ќжидание следующего кадра

        productImages = GetComponentsInChildren<Image>().ToList();
        productPrices = GetComponentsInChildren<TextMeshProUGUI>().ToList();

        for (int i = 1, j = 0; j < inventoryItems.Count; i += 2, j++)
        {
            productImages[i].sprite = itemsToSell[itemsToSellNames.IndexOf(inventoryItemsNames[j])].data.icon;
        }

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            productPrices[i].text = (itemsToSell[itemsToSellNames.IndexOf(inventoryItemsNames[i])].data.sellPrice / 2).ToString();
        }

        List<Button> buttons = GetComponentsInChildren<Button>().ToList();
        foreach (Button b in buttons)
        {
            b.onClick.AddListener(delegate { selectProduct(b); });
        }
        selectedItem.text = "";
    }

    public void sellProduct()
    {
        if (selectedIndex != -1)
        {
            foreach (itemLoc item in inventoryItemsLocs[selectedIndex])
            {
                if (item.count > 0)
                {
                    if (item.inv == "backpack")
                    {
                        player.inventory.backpack.Remove(item.index);
                    }
                    else
                    {
                        player.inventory.toolbar.Remove(item.index);
                    }
                    inventoryItems[inventoryItemsNames[selectedIndex]]--;
                    item.count--;
                    GameManager.instance.uiManager.RefreshAll();
                    GameManager.instance.moneyManager.addMoney(int.Parse(productPrices[selectedIndex].text));
                    if (item == inventoryItemsLocs[selectedIndex][inventoryItemsLocs[selectedIndex].Count - 1] && inventoryItems[inventoryItemsNames[selectedIndex]] == 0)
                    {
                        refreshItemsForSale();
                        return;
                    }
                    return;
                }
                else 
                {
                    if (item == inventoryItemsLocs[selectedIndex][inventoryItemsLocs[selectedIndex].Count - 1])
                    {
                        refreshItemsForSale();
                        return;
                    }
                    continue;
                }
            }
        }
    }

    private void selectProduct(Button button)
    {
        if (selectedIndex != int.Parse(button.GetComponentInChildren<Text>().text))
        {
            selectedIndex = int.Parse(button.GetComponentInChildren<Text>().text);
            selectedItem.text = itemsToSell[itemsToSellNames.IndexOf(inventoryItemsNames[selectedIndex])].data.itemName;
        }
    }
}
