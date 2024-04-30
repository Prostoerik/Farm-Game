using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public void LoadData()
    {
        GameManager.instance.player.nickname = WebManager.userData.playerData.nickname;

        GameManager.instance.player.lvl = WebManager.userData.playerData.lvl;
        GameManager.instance.lvlManager.levelUpdate();

        GameManager.instance.player.money = WebManager.userData.playerData.balance;
        GameManager.instance.moneyManager.moneyUpdate();

        GameManager.instance.player.id = WebManager.userData.playerData.id;
    }


    public void SaveData()
    {
        var nickname = GameManager.instance.player.nickname;
        var lvl = GameManager.instance.player.lvl;
        var balance = GameManager.instance.player.money;
        var id = GameManager.instance.player.id;
        WebManager.instance.SaveData(id, nickname, balance, lvl);
    }
}
