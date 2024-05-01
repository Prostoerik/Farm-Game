using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public string backpack;
    public string toolbar;
    [System.NonSerialized]
    public Inventory backpackData;
    [System.NonSerialized]
    public Inventory toolbarData;
}

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotCount;

    private void Awake()
    {
        if (WebManager.userData.inventoryData.backpackData.slots.Count != 0)
            backpack = WebManager.userData.inventoryData.backpackData;
        else
            backpack = new Inventory(backpackSlotCount);
       
        if (WebManager.userData.inventoryData.toolbarData.slots.Count != 0)
            toolbar = WebManager.userData.inventoryData.toolbarData;
        else
            toolbar = new Inventory(toolbarSlotCount);

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolbar);
    }

    public void Add(string inventoryName, Item item)
    {
        if(inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].Add(item);
        }
    }

    public Inventory GetInventoryByName(string inventoryName)
    {
        if(inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }
        return null;
    }
}
