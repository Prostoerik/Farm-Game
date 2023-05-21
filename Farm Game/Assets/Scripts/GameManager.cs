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

        player = FindObjectOfType<Player>();
    }
}
