using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DestructibleLight))]
public class LightFieldVis : Editor
{    
    private void OnSceneGUI()
    {
        DestructibleLight Light = (DestructibleLight)target;
        Handles.color = new Color(1, 1, 1, Light.visInt/100);
        Handles.DrawSolidArc(Light.transform.position, Vector3.back, ( -Vector3.left*Mathf.Sin(Light.Angle / 2*Mathf.Deg2Rad) - Vector3.up* Mathf.Cos(Light.Angle/ 2 * Mathf.Deg2Rad)), Light.Angle, Light.LightRad);
    }
}
