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
            Debug.Log("������� �� ������");
            SceneManager.LoadScene(1);
        }
        if (sceneIndex == 1)
        {
            Debug.Log("������� �� ������");
            SceneManager.LoadScene(0);
        }
    }
}
