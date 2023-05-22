using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market_UI : MonoBehaviour
{
    public GameObject market;

    void Start()
    {
        market.SetActive(false);
    }

    public void CloseMarket()
    {
        if (market != null)
        {
            market.SetActive(false);
            GameManager.instance.isMarketOpen = false;
        }
    }
}
