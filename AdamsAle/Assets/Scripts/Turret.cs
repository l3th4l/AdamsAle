using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public bool Alert = false;
    public Vector3 AlertPos = Vector3.zero;
    public bool isCam = false;
    public bool isGuard = false;

    public float maxSearchAngle = 360.0f;

    Camera TurretCam;

    Collider Player;

    Plane[] CamPlanes;

    public LayerMask RaycastMask;

    private void Start()
    {
        TurretCam = GetComponentInChildren<Camera>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    private void Update()
    {

        bool PlayerNotObstructed = true;// Variable to check if player is obstructed or not 
        RaycastHit _P_Hit;// Player hit info
        if (Physics.Raycast(TurretCam.transform.position, (Player.transform.position - TurretCam.transform.position).normalized, out _P_Hit, RaycastMask))// Raycasts to the player
        {
            PlayerNotObstructed = (_P_Hit.collider.CompareTag("Player") || (Vector3.SqrMagnitude((transform.position - Player.transform.position).x * Vector3.right) <= TurretCam.nearClipPlane * TurretCam.nearClipPlane && Player.gameObject.activeInHierarchy));// If player isn't obstructed, becomes true
        }

        if (isCam || isGuard)
        {
            CamPlanes = GeometryUtility.CalculateFrustumPlanes(TurretCam);
            if (GeometryUtility.TestPlanesAABB(CamPlanes, Player.bounds) && Player.gameObject.activeInHierarchy && PlayerNotObstructed)// Checks if player is in LOS
            {
                Alert = true;
            }
        }

        if(Alert)
        {
            CamPlanes = GeometryUtility.CalculateFrustumPlanes(TurretCam);
            if (GeometryUtility.TestPlanesAABB(CamPlanes, Player.bounds) && Player.gameObject.activeInHierarchy && PlayerNotObstructed)// Checks if player is in LOS
            {
                float Angle = Vector3.Angle(Vector3.right, (Player.transform.position - transform.position).normalized);// Angle between Camera and player's position
                transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Clamp(Mathf.Round(Angle / 4) * 4, -maxSearchAngle, 90)); // makes the camera look at player's position
            }
        }
    }
    void search()
    {

    }
}
