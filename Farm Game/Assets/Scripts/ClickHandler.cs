using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickHandler : MonoBehaviour
{
    private void OnMouseDown()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        {
            Debug.Log("Нажатие на объект");
            //SceneManager.LoadScene("Fishing", LoadSceneMode.Additive);
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Fishing"));
            //SceneManager.UnloadSceneAsync(0);
            SceneManager.LoadScene(1);
        }
        if (sceneIndex == 1)
        {
            Debug.Log("Нажатие на объект");
            SceneManager.LoadScene(0);
            //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            //SceneManager.UnloadSceneAsync("Fishing");
        }
    }
}
