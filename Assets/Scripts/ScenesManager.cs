using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private int mainSceneIndex = 1;
    private int menuSceneIndex = 0;
   public void LoadMainScene()
    {
        SceneManager.LoadScene(mainSceneIndex);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }
}
