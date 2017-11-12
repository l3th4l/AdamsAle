using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    public float Tm;
    public float Speed;
    public GameObject Alarm;
    GameObject Player;
    Camera ChildCam;
    Plane[] CamPlanes;
    private void Start()
    {
        ChildCam = GetComponentInChildren<Camera>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update ()
    {
        Tm += Time.deltaTime * Speed;
        transform.rotation = Quaternion.Euler(0.0f, Mathf.Clamp(Mathf.Sin(Tm) *120,-90.0f,90.0f) , 0.0f);

        CamPlanes = GeometryUtility.CalculateFrustumPlanes(ChildCam);
        if (GeometryUtility.TestPlanesAABB(CamPlanes, Player.GetComponent<CapsuleCollider>().bounds) && Player.activeInHierarchy)
        {
            print("Player Seen");
            Alarm.GetComponent<AlarmSyst>().Alert = true;
            Alarm.GetComponent<AlarmSyst>().AlertTime = Time.deltaTime;
        }
	}
}
