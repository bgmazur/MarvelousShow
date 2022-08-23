using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainScene() {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void LoadTvOpening() {
        SceneManager.LoadScene("TVOpening", LoadSceneMode.Single);
    }

    public void Exit() {
        Application.Quit();
    }
}
