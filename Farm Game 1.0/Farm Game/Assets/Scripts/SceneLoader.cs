using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName = "Game"; // Название сцены, в которую вы хотите перейти. Можете задать его в инспекторе или через код.

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
