using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Order : MonoBehaviour
{
    List<Image> productImages;
    List<TextMeshProUGUI> productCount;

    List<Item> required = new List<Item>();
    int reward = 0;

    List<string> requiredItemNames = new List<string>();
    List<int> requiredItemCount = new List<int>();

    Dictionary<int, int> inventoryRemoveItems = new Dictionary<int, int>();
    Dictionary<int, int> toolbarRemoveItems = new Dictionary<int, int>();

    public List<Item> items;
    public Sprite empty;
    public Player player;
    public Button sellButton;
    public Button changeButton;
    public TextMeshProUGUI rewardText;

    void Start()
    {
        productImages = GetComponentsInChildren<Image>().ToList();
        productCount = GetComponentsInChildren<TextMeshProUGUI>().ToList();

        for (int i = 2; i < productImages.Count; i += 2)
        {
            productImages[i].sprite = empty;
        }
        for (int i = 0; i < productCount.Count; i++)
        {
            productCount[i].text = "";
        }

        sellButton.onClick.AddListener(delegate { completeOrder(); });
        changeButton.onClick.AddListener(delegate { changeOrder(); });

        createOrder();
    }

    private void createOrder()
    {
        int numItemsToSelect = 3;
        if (Player.lvl > 1) numItemsToSelect += Player.lvl;
        if (numItemsToSelect > 8) numItemsToSelect = Random.Range(numItemsToSelect / 2, numItemsToSelect);
        else numItemsToSelect = Random.Range(2, numItemsToSelect + 1);

        List<int> selectedIndexes = new List<int>();
        reward = 0;

        while (required.Count < numItemsToSelect)
        {
            int randomIndex = Random.Range(0, items.Count); 

            if (!selectedIndexes.Contains(randomIndex) && items[randomIndex].data.openingLevel <= Player.lvl)
            {
                required.Add(items[randomIndex]);
                selectedIndexes.Add(randomIndex);
            }
        }

        for (int i = 0; i < required.Count; i++)
        {
            requiredItemNames.Add(required[i].data.itemName);

            int randomCount = Random.Range(1, 5);
            requiredItemCount.Add(randomCount);
        }

        for (int i = 0, j = 2; i < required.Count; i++, j += 2)
        {
            productImages[j].sprite = required[i].data.icon;
            productCount[i].text = requiredItemCount[i].ToString();
        }

        for (int i = 0; i < required.Count; i++)
        {
            reward += required[i].data.sellPrice * requiredItemCount[i];
        }
        rewardText.text = reward + "$";
    }

    private void completeOrder()
    {
        if (isEnoughItems())
        {
            Debug.Log("Достаточно");
            foreach (var v in inventoryRemoveItems)
            {
                player.inventory.backpack.Remove(v.Key, v.Value);
            }
            foreach (var v in toolbarRemoveItems)
            {
                player.inventory.toolbar.Remove(v.Key, v.Value);
            }
            GameManager.instance.uiManager.RefreshAll();
            GameManager.instance.moneyManager.addMoney(reward);
            Player.addExp(1);
            refreshOrder();
        }
        else
        {
            Debug.Log("Нет");
        }
    }

    private void changeOrder()
    {
        refreshOrder();
    }

    private void refreshOrder()
    {
        required.Clear();

        requiredItemNames.Clear();
        requiredItemCount.Clear();

        inventoryRemoveItems.Clear();
        toolbarRemoveItems.Clear();

        for (int i = 2; i < productImages.Count; i += 2)
        {
            productImages[i].sprite = empty;
        }
        for (int i = 0; i < productCount.Count; i++)
        {
            productCount[i].text = "";
        }

        createOrder();
    }

    public bool isEnoughItems()
    {
        Dictionary<string, int> tempInv = new Dictionary<string, int>();

        foreach (var v in requiredItemNames)
        {
            tempInv.Add(v, 0);
        }

        int j = 0;
        foreach (var v in requiredItemNames)
        {
            for (int i = 0; i < 27; i++)
            {
                if (!player.inventory.backpack.slots[i].IsEmpty && player.inventory.backpack.slots[i].itemName == v)
                {
                    if (tempInv[v] == requiredItemCount[j]) break;
                    if (requiredItemCount[j] - tempInv[v] > player.inventory.backpack.slots[i].count)
                    {
                        inventoryRemoveItems.Add(i, player.inventory.backpack.slots[i].count);
                        tempInv[v] += player.inventory.backpack.slots[i].count;
                    }
                    else
                    {
                        inventoryRemoveItems.Add(i, requiredItemCount[j] - tempInv[v]);
                        tempInv[v] += requiredItemCount[j] - tempInv[v];
                    }
                }
            }
            if (tempInv[v] != requiredItemCount[j])
            {
                for (int i = 0; i < 9; i++)
                {
                    if (!player.inventory.toolbar.slots[i].IsEmpty && player.inventory.toolbar.slots[i].itemName == v)
                    {
                        if (tempInv[v] == requiredItemCount[j]) break;
                        if (requiredItemCount[j] - tempInv[v] > player.inventory.toolbar.slots[i].count)
                        {
                            toolbarRemoveItems.Add(i, player.inventory.toolbar.slots[i].count);
                            tempInv[v] += player.inventory.toolbar.slots[i].count;
                        }
                        else
                        {
                            toolbarRemoveItems.Add(i, requiredItemCount[j] - tempInv[v]);
                            tempInv[v] += requiredItemCount[j] - tempInv[v];
                        }
                    }
                }
            }
            if (tempInv[v] != requiredItemCount[j]) {

                inventoryRemoveItems.Clear();
                toolbarRemoveItems.Clear();
                return false; 
            }
            j++;
        }

        return true;
    }
}
