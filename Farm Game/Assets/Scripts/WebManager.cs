using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class UserData
{
    public PlayerData playerData;
    public Error error;
}

[System.Serializable]
public class Error
{
    public string errorText;
    public bool isError;
}

public class WebManager : MonoBehaviour
{
    public static WebManager instance;
    public static UserData userData = new UserData();
    [SerializeField] private string targetURL;

    [SerializeField] private UnityEvent OnLogged, OnRegistered, OnError;

    public enum RequestType
    {
        logging, register, save
    }

    public string GetUserData(UserData data)
    {
        return JsonUtility.ToJson(data);
    }

    public UserData SetUserData(string data)
    {
        return JsonUtility.FromJson<UserData>(data);
    }

    //private void Start()
    //{
    //    userData.error = new Error() { errorText = "text", isError = true };
    //    userData.playerData = new PlayerData() { lvl = GameManager.instance.player.lvl, balance = GameManager.instance.player.money };
    //    //print(GetUserData(userData));
    //}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Login(string login, string password)
    {
        StopAllCoroutines();
        if (CheckString(login) && CheckString(password))
        {
            Logging(login, password);
        }
        else
        {
            userData.error.errorText = "Too small length";
            OnError.Invoke();
        }
    }

    public void Registration(string login, string password, string password2, string nickname)
    {
        StopAllCoroutines();
        if (CheckString(login) && CheckString(password) && CheckString(password2) && CheckString(nickname) && password == password2)
        {
            Debug.Log("Here");
            Registering(login, password, password2, nickname);
        }
        else
        {
            userData.error.errorText = "Too small length";
            OnError.Invoke();
        }
    }

    public void SaveData(int id, string nickname, int balance, int level)
    {
        StopAllCoroutines();
        SaveProgress(id, nickname, balance, level);
    }

    public bool CheckString(string toCheck)
    {
        toCheck = toCheck.Trim();
        if (toCheck.Length > 2 && toCheck.Length < 16)
        {
            return true;
        }
        return false;
    }

    public void Logging(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.logging.ToString());
        form.AddField("login", login);
        form.AddField("password", password);
        StartCoroutine(SendData(form, RequestType.logging));
    }

    public void Registering(string login, string password1, string password2, string nickname)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.register.ToString());
        form.AddField("login", login);
        form.AddField("password1", password1);
        form.AddField("password2", password2);
        form.AddField("nickname", nickname);
        StartCoroutine(SendData(form, RequestType.register));
    }

    public void SaveProgress(int id, string nickname, int balance, int level)
    {
        Debug.Log("Here");
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.save.ToString());
        form.AddField("id", id);
        form.AddField("balance", balance);
        form.AddField("level", level);
        form.AddField("nickname", nickname);
        StartCoroutine(SendData(form, RequestType.save));
    }

    IEnumerator SendData(WWWForm form, RequestType type)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(targetURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var data = SetUserData(www.downloadHandler.text);
                if (!data.error.isError)
                {
                    if (type != RequestType.save)
                    {
                        userData = data;
                        if (type == RequestType.logging)
                        {
                            print(GetUserData(userData));
                            OnLogged.Invoke();
                        }
                        else
                        {
                            OnRegistered.Invoke();
                        }
                    }
                }
                else
                {
                    userData = data;
                    OnError.Invoke();
                }
            }
        }
    }
}


