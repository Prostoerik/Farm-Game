using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk_UI : MonoBehaviour
{
    public GameObject desk;

    void Start()
    {
        desk.SetActive(false);
    }

    public void CloseDesk()
    {
        if (desk != null)
        {
            desk.SetActive(false);
            GameManager.instance.isDeskOpen = false;
        }
    }
}
