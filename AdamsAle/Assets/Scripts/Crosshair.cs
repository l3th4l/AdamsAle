using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
    void LateUpdate ()
    {
        GetComponent<RectTransform>().position = Input.mousePosition;
	}
}
