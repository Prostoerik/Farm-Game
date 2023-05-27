using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();

    public GameObject inventoryPanel;

    public List<Inventory_UI> inventoryUIs;

    public static Slots_UI draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;

    private void Awake()
    {
        Initialize();
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.B)) && !GameManager.instance.isDeskOpen && !GameManager.instance.isMarketOpen)
        {
            ToggleInventoryUI();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }
    }

    public void ToggleInventoryUI()
    {
        if (inventoryPanel != null)
        {
            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
                GameManager.instance.isInventoryOpen = true;
                RefreshInventoryUI("Backpack");
            }
            else
            if (!GameManager.instance.isDragging)
            {
                inventoryPanel.SetActive(false);
                GameManager.instance.isInventoryOpen = false;
            }
        }
    }

    public void RefreshInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            inventoryUIByName[inventoryName].Refresh();
        }
    }

    public void RefreshAll()
    {
        foreach(var keyValuePair in inventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        }
        GameManager.instance.isBackpackCapable = false;
        foreach (Inventory.Slot slot in GameManager.instance.player.inventory.backpack.slots)
        {
            if (slot.IsEmpty)
            {
                GameManager.instance.isBackpackCapable = true;
                break;
            }
        }
        GameManager.instance.isToolbarCapable = false;
        foreach (Inventory.Slot slot in GameManager.instance.player.inventory.toolbar.slots)
        {
            if (slot.IsEmpty)
            {
                GameManager.instance.isToolbarCapable = true;
                break;
            }
        }
    }

    public Inventory_UI GetInventoryUI(string inventoryName)
    {
        if(inventoryUIByName.ContainsKey(inventoryName))
        {
            return inventoryUIByName[inventoryName];
        }

        Debug.LogWarning("There is not inventory UI for " + inventoryName);
        return null;
    }

    void Initialize()
    {
        foreach(Inventory_UI ui in inventoryUIs)
        {
            if(!inventoryUIByName.ContainsKey(ui.inventoryName))
            {
                inventoryUIByName.Add(ui.inventoryName, ui);
            }
        }
    }
}
