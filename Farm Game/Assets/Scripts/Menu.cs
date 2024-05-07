using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [System.Serializable]
    public class MenuLogin
    {
        public TMP_Text login, password;
    }
    [System.Serializable]
    public class MenuRegistration
    {
        public TMP_Text login, password1, password2, nickname;
    }

    public MenuLogin loginWindow;
    public MenuRegistration registrationWindow;
    public GameObject loginPanel;
    public GameObject signupPanel;
    public GameObject authPanel;

    [SerializeField] private WebManager webManager;

    public void Awake()
    {
        authPanel.SetActive(false);
    }

    public void Login()
    {
        print(loginWindow.login.text);
        print(loginWindow.password.text);
        webManager.Login(loginWindow.login.text, loginWindow.password.text);
    }

    public void Register()
    {
        webManager.Registration(registrationWindow.login.text, registrationWindow.password1.text, registrationWindow.password2.text, registrationWindow.nickname.text);
    }

    public void ShowLoginPanel()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
    }

    public void ShowSignupPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
    }

    public void StartGame()
    {
        int userID = WebManager.GetUserID();
        print(userID);

        if (userID == -1)
        {
            OpenAuthPanel();
        }
        else
        {
            webManager.LoginByID(userID);
        }
    }

    public void OpenAuthPanel()
    {
        authPanel.SetActive(true);
    }

    public void CloseAuthPanel()
    {
        authPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
