using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI marketBalance;

    public void moneyUpdate()
    {
        if (Player.money != int.Parse(moneyText.text.Substring(0, moneyText.text.Length - 1))) 
        {
            moneyText.text = Player.money.ToString() + "$";
            marketBalance.text = moneyText.text;
        }
    }

    public void addMoney(int amount)
    {
        Player.money += amount;
        moneyUpdate();
    }

    public void reduceMoney(int amount)
    {
        Player.money -= amount;
        moneyUpdate();
    }
}
