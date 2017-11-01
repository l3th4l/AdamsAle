using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimRotation : MonoBehaviour {

    public float rotAngle;

    void Update()
    {
        //rotation
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        rotAngle = angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
