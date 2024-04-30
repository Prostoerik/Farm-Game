using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName = "Fishing"; // Название сцены, в которую вы хотите перейти. Можете задать его в инспекторе или через код.

    public void LoadScene()
    {
        //SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadSceneAsync());
    }

    public Image loadingIcon;

    IEnumerator LoadSceneAsync()
    {
        yield return null;

        loadingIcon.enabled = true; // Показать значок загрузки

        // Загрузка сцены
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        // Ждем завершения загрузки
        while (!asyncLoad.isDone)
        {
            // Прогресс загрузки (от 0 до 1)
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // Можно использовать этот прогресс для обновления прогресс-бара или анимации значка загрузки
            loadingIcon.fillAmount = progress;

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

       /* loadingIcon.enabled = false;*/ // Скрыть значок загрузки после завершения загрузки
    }
}
