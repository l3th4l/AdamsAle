using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{

    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minimunLogoTime = 3.0f;

    public string sceneToLoad = "MainMenu";

    void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 1;

        if (Time.time < minimunLogoTime)
            loadTime = minimunLogoTime;
        else
            loadTime = Time.time;
    }

    void Update()
    {
        // fade-In

        if (Time.time < minimunLogoTime)
        {
            fadeGroup.alpha = 1 - Time.time;
        }

        // fade-out

        if (Time.time > minimunLogoTime && loadTime != 0)
        {
            fadeGroup.alpha = Time.time - minimunLogoTime;
            if (fadeGroup.alpha >= 1)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }

    }
}