using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ItemManager itemManager;
    public TileManager tileManager;
    public UI_Manager uiManager;
    public CropsManager cropsManager;
    public LevelManager lvlManager;
    public MoneyManager moneyManager;

    public Player player;

    public int selectedItemIndex;

    public bool isInventoryOpen;
    public bool isDeskOpen;
    public bool isMarketOpen;

    public bool isDragging;
    public bool isBackpackCapable;
    public bool isToolbarCapable;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        itemManager = GetComponent<ItemManager>();
        tileManager = GetComponent<TileManager>();
        uiManager = GetComponent<UI_Manager>();
        cropsManager = GetComponent<CropsManager>();
        lvlManager = GetComponent<LevelManager>();
        moneyManager = GetComponent<MoneyManager>();
        selectedItemIndex = 0;
        isInventoryOpen = false;
        isDeskOpen = false;
        isMarketOpen = false;
        isDragging = false;
        isBackpackCapable = true;
        isToolbarCapable = true;

        player = FindObjectOfType<Player>();
    }
}
