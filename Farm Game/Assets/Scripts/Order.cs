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
    public List<Item> items;
    public Sprite empty;

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

        //GetComponentInChildren<Button>().onClick.AddListener(delegate { completeOrder(); });

        createOrder();
    }

    private void createOrder()
    {
        required.Add(items[0]);
        required.Add(items[1]);

        for (int i = 0; i < required.Count; i++)
        {
            if (i == 0) productImages[i + 2].sprite = required[i].data.icon;
            else productImages[i + 3].sprite = required[i].data.icon;
            //productCount[i + 2].Quantity = "x" + required[i].count.ToString();
            productCount[i].text = "1";
        }

    }

    private void completeOrder()
    {

    }
}
