using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;

    private bool isMenuOpen;
    
    void Start()
    {
        isMenuOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
        isMenuOpen = true;
        Time.timeScale = 1f; // Останавливаем игровое время
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        isMenuOpen = false;
        Time.timeScale = 1f; // Возобновляем игровое время
    }

    public void ContinueGame()
    {
        CloseMenu();
        // Дополнительные действия для продолжения игры
    }

    public void OpenMainMenu()
    {
        // Дополнительные действия для открытия главного меню
    }

    public void OpenSettings()
    {
        // Дополнительные действия для открытия настроек
    }

    public void GoBack()
    {
        // Дополнительные действия для возврата назад в меню
    }
}