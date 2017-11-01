using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {

    public Image fadeImage;
    [Tooltip("Lower the value is, more time it takes to fade")]
    public float fadeSpeed = 0.5f;
	public static bool faded = false;

    void Start()
    {
		if (!faded) 
		{
			fadeImage.enabled = true;
			StartCoroutine(FadeIn());
			faded = true;
		}
        
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0f, 0f, 0f, t);
            yield return 0;
        }
        if(t <= 0f)
        {
            fadeImage.enabled = false;
        }
    }
}
