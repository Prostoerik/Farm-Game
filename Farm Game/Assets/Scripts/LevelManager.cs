using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Image levelBar;
    public TextMeshProUGUI levelText;

    public void levelUpdate()
    {
        if (GameManager.instance.player.lvl != int.Parse(levelText.text)) 
        {
            levelText.text = GameManager.instance.player.lvl.ToString();
        }
        levelBar.fillAmount = Player.lvlProgress;
    }
}
