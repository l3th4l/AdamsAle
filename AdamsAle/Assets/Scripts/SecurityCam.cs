using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    public float Tm;
    public float Speed;    
    public Vector3 axis;
    public float offset;
    float RotTime;

    public float SearchAngle;
    LightVis Visualizer;
    private void Start()
    {
        Visualizer = GetComponent<LightVis>();
    }
    void Update ()
    {
        if (!Visualizer.SeenPlayer)
            Tm += Time.deltaTime;
        transform.localEulerAngles = axis * (Mathf.Clamp(Mathf.Sin(Tm) * 1.5f, -1, 1) * SearchAngle + offset);
	}
    
}
