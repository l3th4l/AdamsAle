using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Private Variables")]
    Camera AICam;
    Plane[] CamPlanes;
    Collider Player;
    public bool alert;

    float time;
    public float crisp_factor;

    public Transform Parent;

    public float offset;
    public float maxSearchAngle; // maximum rotation of camera while searching

    private void Start()
    {
        AICam = transform.GetChild(0).GetComponentInChildren<Camera>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        time = 0.0f;
    }
    private void Update()
    {

       time += Time.deltaTime; 

       if (alert)// Look at player if alert 
       {
            float Angle = Vector3.Angle(transform.right, (Player.transform.position - transform.position));// Angle between Camera and distraction position
            AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Clamp(Mathf.Round(Angle / crisp_factor) * crisp_factor, -maxSearchAngle, maxSearchAngle)); // makes the camera look at distraction position
            time = 0.0f;
       }
        else
            Idle(true, time);
    }
    void Idle(bool Guard , float TM)
    {
        AICam.transform.parent.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Round((Mathf.Clamp(Mathf.Sin(TM)*5,-1,1)*90 + offset)/crisp_factor)*crisp_factor ) ; // makes the camera look at distraction position
    }
}
