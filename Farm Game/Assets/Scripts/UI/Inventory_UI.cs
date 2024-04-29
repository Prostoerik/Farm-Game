using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public string inventoryName; 

    public List<Slots_UI> slots = new List<Slots_UI>();

    [SerializeField] private Canvas canvas;

    private bool dragSingle;

    private Inventory inventory;
    
    
    //SOUND
    public AudioClip openInventorySound;
    public AudioClip closeInventorySound;
    private AudioSource audioSource;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Воспроизводим звук открытия инвентаря
                audioSource.clip = openInventorySound;
                audioSource.Play();
        }
    }

    public void CloseInventory()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
            audioSource.clip = closeInventorySound;
            audioSource.Play();
            GameManager.instance.isInventoryOpen = false;
        }
    }

  
    void Start()
    {
        //inventoryPanel.SetActive(false);
        inventory = GameManager.instance.player.inventory.GetInventoryByName(inventoryName);
        Debug.Log(inventoryName);

        SetupSlots();
        Refresh();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = 0.3f; // Установите громкость звука 0.3f

        // Присваиваем звуки открытия и закрытия инвентаря
        audioSource.clip = openInventorySound;
        audioSource.clip = closeInventorySound;
    }

    public void Refresh()
    {
        if(slots.Count == inventory.slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if (inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            } 
        }
    }

    public void Remove()
    {
        try
        {
            Item itemToDropTry = GameManager.instance.itemManager.GetItemByName(inventory.slots[UI_Manager.draggedSlot.slotID].itemName);
        }
        catch (Exception ex)
        {
            string t = ex.ToString();
            return;
        }

        //Item itemToDrop = GameManager.instance.itemManager.GetItemByName(inventory.slots[UI_Manager.draggedSlot.slotID].itemName);
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(UI_Manager.draggedSlot.inventory.slots[UI_Manager.draggedSlot.slotID].itemName);
        inventory = UI_Manager.draggedSlot.inventory;

        if (itemToDrop != null)
        {
            if (UI_Manager.dragSingle)
            {
                GameManager.instance.player.DropItem(itemToDrop);
                inventory.Remove(UI_Manager.draggedSlot.slotID);
            }
            else
            {
                GameManager.instance.player.DropItem(itemToDrop, inventory.slots[UI_Manager.draggedSlot.slotID].count);
                inventory.Remove(UI_Manager.draggedSlot.slotID, inventory.slots[UI_Manager.draggedSlot.slotID].count);
            }
            GameManager.instance.uiManager.RefreshAll();
        }
        inventory = GameManager.instance.player.inventory.GetInventoryByName(inventoryName);
        UI_Manager.draggedSlot = null;
    }

    public void SlotBeginDrag(Slots_UI slot)
    {
        GameManager.instance.isDragging = true;

        UI_Manager.draggedSlot = slot;
        UI_Manager.draggedIcon = Instantiate(UI_Manager.draggedSlot.itemIcon);
        UI_Manager.draggedIcon.raycastTarget = false;
        UI_Manager.draggedIcon.rectTransform.sizeDelta = new Vector2(50f, 50f);
        UI_Manager.draggedIcon.transform.SetParent(canvas.transform);

        MoveToMousePosition(UI_Manager.draggedIcon.gameObject);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(UI_Manager.draggedIcon.gameObject);
    }

    public void SlotEndDrag()
    {
        Destroy(UI_Manager.draggedIcon.gameObject);
        GameManager.instance.isDragging = false;
        //UI_Manager.draggedIcon = null;
    }

    public void SlotDrop(Slots_UI slot)
    {
        if(UI_Manager.dragSingle)
        { 
            UI_Manager.draggedSlot.inventory.MoveSlot(UI_Manager.draggedSlot.slotID, slot.slotID, slot.inventory);
        }
        else
        {
            UI_Manager.draggedSlot.inventory.MoveSlot(UI_Manager.draggedSlot.slotID, slot.slotID, slot.inventory, UI_Manager.draggedSlot.inventory.slots[UI_Manager.draggedSlot.slotID].count);
        }
        GameManager.instance.uiManager.RefreshAll();
    }

    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            toMove.transform.position = canvas.transform.TransformPoint(position); 
        }
    }

    void SetupSlots()
    {
        int counter = 0;

        foreach(Slots_UI slot in slots)
        {
            slot.slotID = counter;
            counter++;
            slot.inventory = inventory;
        }
    }
}
