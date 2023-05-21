using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryManager inventory;

    public static int lvl;
    public static float lvlProgress;
    public static float[] expMultiplier = new float[8];
    public static int money = 200;

    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
    }

    private void Start()
    {
        lvl = 1;
        lvlProgress = 0;
        expMultiplier[1] = 0.04f;
        expMultiplier[2] = 0.03f;
        expMultiplier[3] = 0.03f;
        expMultiplier[4] = 0.02f;
        expMultiplier[5] = 0.02f;
        expMultiplier[6] = 0.01f;
        expMultiplier[7] = 0.01f;
        GameManager.instance.lvlManager.levelUpdate();
        GameManager.instance.moneyManager.moneyUpdate();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
            if(GameManager.instance.tileManager.isInteractable(position))
            {
                //Debug.Log("Tile is interactable");
                //inventory.toolbar.slots[GameManager.instance.uiManager.inventoryUIs[1].selectedSlot.slotID].itemName == "Carrot_seeds"
                //inventory.toolbar.slots[GameManager.instance.selectedItemIndex].itemName == "Carrot seeds"
                if (GameManager.instance.tileManager.isPlowed(position) && inventory.toolbar.slots[GameManager.instance.selectedItemIndex].itemName == "Carrot seeds")
                {
                    inventory.toolbar.slots[GameManager.instance.selectedItemIndex].RemoveItem();
                    //inventory.toolbar.MoveSlot(GameManager.instance.selectedItemIndex, 26, inventory.backpack);
                    GameManager.instance.uiManager.RefreshAll();
                    //inventory.toolbar.Remove(GameManager.instance.selectedItemIndex);
                    //Debug.Log(inventory.toolbar.slots[GameManager.instance.selectedItemIndex].itemName); 
                    GameManager.instance.cropsManager.SetPlanted(position);
                }
                else
                {
                    GameManager.instance.tileManager.SetInteracted(position);
                }
            }
        }
    }

    public static void addExp(int exp)
    {
        lvlProgress += exp * expMultiplier[lvl];

        while (lvlProgress >= 1)
        {
            lvl++;
            lvlProgress--;
        }

        GameManager.instance.lvlManager.levelUpdate();
    }

    public void DropItem(Item item)
    {
        Vector3 spawnLocation = transform.position;

        float R = 1;
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float x = Mathf.Cos(angle) * R;
        float y = Mathf.Sin(angle) * R;
        Vector3 spawnOffset = new Vector3(x, y, 0f);

        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset * 1.2f, Quaternion.identity);

        //droppedItem.rb2d.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
    }

    public void DropItem(Item item, int numToDrop)
    {
        for(int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }
}
