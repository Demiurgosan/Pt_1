using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_NextLvl : MonoBehaviour
{
    public void OnBottonClick()
    {
        if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
