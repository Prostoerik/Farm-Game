using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Purchase : MonoBehaviour
{
    List<Image> productImages;
    List<TextMeshProUGUI> productPrices;
    List<Button> buttons;

    private int curLevel = 0;

    private int selectedIndex = -1;

    public List<Item> itemsToSell;

    public GameObject panel;
    public GameObject purchaseSlot;

    public Player player;
    public Sprite empty;
    public Button buyButton;

    public TextMeshProUGUI selectedItem;
    public TextMeshProUGUI balance;

    void Start()
    {
        foreach (Transform child in panel.transform)
        {
            GameObject.Destroy(child.gameObject, 0);
        }

        int index = 0;
        for (int i = 0; i < itemsToSell.Count; i++)
        {
            GameObject instantiatedPrefab = Instantiate(purchaseSlot);
            instantiatedPrefab.transform.SetParent(panel.transform, false);
            instantiatedPrefab.GetComponentInChildren<Text>().text = index.ToString();
            index++;
        }

        productImages = GetComponentsInChildren<Image>().ToList();
        productPrices = GetComponentsInChildren<TextMeshProUGUI>().ToList();

        for (int i = 1, j = 0; i < productImages.Count; i += 3, j++)
        {
            productImages[i].sprite = itemsToSell[j].data.icon;
        }

        for (int i = 0, j = 0; i < productPrices.Count; i += 2, j++)
        {
            productPrices[i].text = itemsToSell[j].data.buyPrice.ToString();
        }

        buttons = GetComponentsInChildren<Button>().ToList();
        foreach(Button b in buttons)
        {
            b.onClick.AddListener(delegate { selectProduct(b); });
        }
        selectedItem.text = "";

        buyButton.onClick.AddListener(delegate { buyProduct(); });
    }

    private void FixedUpdate()
    {
        if (curLevel < Player.lvl)
        {
            for (int i = 0, j = 1, k = 2; i < itemsToSell.Count; i++, j += 2, k += 3)
            {
                if (itemsToSell[i].data.openingLevel > Player.lvl)
                {
                    buttons[i].onClick.RemoveAllListeners();
                    productPrices[j].enabled = true;
                    productPrices[j].text = "Lvl" + itemsToSell[i].data.openingLevel.ToString();
                    productImages[k].enabled = true;
                }
                else
                {
                    int tempIndex = i;
                    productPrices[j].enabled = false;
                    productImages[k].enabled = false;

                    buttons[i].onClick.RemoveAllListeners();
                    buttons[i].onClick.AddListener(delegate { selectProduct(buttons[tempIndex]); });
                }
            }
            curLevel = Player.lvl;
        }
    }


    private void buyProduct()
    {
        if (selectedIndex != -1 && int.Parse(productPrices[selectedIndex * 2].text) <= int.Parse(balance.text.Substring(0, balance.text.Length - 1)) && GameManager.instance.isBackpackCapable) {
            player.inventory.backpack.Add(itemsToSell[selectedIndex]);
            GameManager.instance.uiManager.RefreshAll();
            GameManager.instance.moneyManager.reduceMoney(int.Parse(productPrices[selectedIndex * 2].text));
        }
    }

    private void selectProduct(Button button)
    {
        if (selectedIndex != int.Parse(button.GetComponentInChildren<Text>().text))
        {
            selectedIndex = int.Parse(button.GetComponentInChildren<Text>().text);
            selectedItem.text = itemsToSell[selectedIndex].data.itemName;
        }
    }
}
