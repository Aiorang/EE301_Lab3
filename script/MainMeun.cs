using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeun : MonoBehaviour
{
    int sceneN;
    private void Start()
    {
        LoadFromPlayerPrefs();
    }
    public void play()
    {
        SceneManager.LoadScene(sceneN);
    }
    public void exit()
    {
        Application.Quit();
    }

    #region PlayerPrefs


    void LoadFromPlayerPrefs()
    {
        sceneN = PlayerPrefs.GetInt("Scene", 1);
    }

    #endregion
}
