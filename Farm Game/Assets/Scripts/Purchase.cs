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

        for (int i = 1, j = 0; i < productImages.Count; i += 2, j++)
        {
            productImages[i].sprite = itemsToSell[j].data.icon;
        }

        for (int i = 0; i < productPrices.Count; i++)
        {
            productPrices[i].text = itemsToSell[i].data.buyPrice.ToString();
        }

        List<Button> buttons = GetComponentsInChildren<Button>().ToList();
        foreach(Button b in buttons)
        {
            b.onClick.AddListener(delegate { selectProduct(b); });
        }
        selectedItem.text = "";

        buyButton.onClick.AddListener(delegate { buyProduct(); });
    }

    private void buyProduct()
    {
        if (selectedIndex != -1 && int.Parse(productPrices[selectedIndex].text) <= int.Parse(balance.text.Substring(0, balance.text.Length - 1))) {
            player.inventory.backpack.Add(itemsToSell[selectedIndex]);
            GameManager.instance.uiManager.RefreshAll();
            GameManager.instance.moneyManager.reduceMoney(int.Parse(productPrices[selectedIndex].text));
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
