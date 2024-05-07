using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class UserData
{
    public PlayerData playerData;
    public InventoryData inventoryData;
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

    private const string USER_ID_KEY = "user_id";

    [SerializeField] private string targetURL;

    [SerializeField] private UnityEvent OnLogged, OnRegistered, OnError;

    public enum RequestType
    {
        logging, loggingByID, register, save
    }

    // Статический метод для получения идентификатора пользователя из PlayerPrefs
    public static int GetUserID()
    {
        return PlayerPrefs.GetInt(USER_ID_KEY, -1); // Возвращаем -1, если значение не найдено
    }

    // Статический метод для сохранения идентификатора пользователя в PlayerPrefs
    public static void SetUserID(int userID)
    {
        PlayerPrefs.SetInt(USER_ID_KEY, userID);
        PlayerPrefs.Save(); // Сохраняем изменения
    }

    // Метод для удаления идентификатора пользователя из PlayerPrefs
    public static void ClearUserID()
    {
        PlayerPrefs.DeleteKey(USER_ID_KEY);
        PlayerPrefs.Save(); // Сохраняем изменения
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

    public void SaveData(int id, string nickname, int balance, int level, float lvlProgress, float x_pos, float y_pos, string backpack, string toolbar)
    {
        StopAllCoroutines();
        SaveProgress(id, nickname, balance, level, lvlProgress, x_pos, y_pos, backpack, toolbar);
    }

    public void LoginByID(int id)
    {
        StopAllCoroutines();
        LoggingByID(id);
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

    public void LoggingByID(int id)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.loggingByID.ToString());
        form.AddField("id", id);
        StartCoroutine(SendData(form, RequestType.loggingByID));
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

    public void SaveProgress(int id, string nickname, int balance, int level, float lvlProgress, float x_pos, float y_pos, string backpack, string toolbar)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.save.ToString());
        form.AddField("id", id);
        form.AddField("balance", balance);
        form.AddField("lvl", level);
        form.AddField("lvlProgress", lvlProgress.ToString());
        form.AddField("x_pos", x_pos.ToString());
        form.AddField("y_pos", y_pos.ToString());
        form.AddField("nickname", nickname);
        form.AddField("backpack", backpack);
        form.AddField("toolbar", toolbar);
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
                www.Dispose();
            }
            else
            {
                print(www.downloadHandler.text);
                var data = SetUserData(www.downloadHandler.text);
                www.Dispose();
                if (!data.error.isError)
                {
                    if (type != RequestType.save)
                    {
                        data.inventoryData.backpackData = JsonUtility.FromJson<Inventory>(data.inventoryData.backpack);
                        data.inventoryData.toolbarData = JsonUtility.FromJson<Inventory>(data.inventoryData.toolbar);
                        userData = data;
                        if (type == RequestType.logging)
                        {
                            print(GetUserData(userData));
                            SetUserID(data.playerData.id);
                            OnLogged.Invoke();
                        }
                        if (type == RequestType.loggingByID)
                        {
                            OnLogged.Invoke();
                        }
                        if (type == RequestType.register)
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


