using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        CurrentScore.currentScore = 0;
        CurrentScore.currentDarkSpirits = 0;
        CurrentScore.currentLightSpirits = 0;
    }

    public void PlayButoon(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
