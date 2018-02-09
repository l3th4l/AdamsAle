using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackUI : MonoBehaviour
{
    public float Progression;

    [HideInInspector]
    public GameObject ProgressUI;

    float UIScale;

    private void Start()
    {
        ProgressUI = transform.GetChild(0).gameObject;
        UIScale = ProgressUI.GetComponent<RectTransform>().localScale.x;
    }

    void LateUpdate ()
    {
        Progression = Mathf.Clamp01(Progression);
        ProgressUI.GetComponent<RectTransform>().localScale = Vector3.one * (1 - Progression) * UIScale;

    }
}
