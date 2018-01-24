using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVisibility : MonoBehaviour {
    
    public Camera LightCam;
    CharacterController Player;
    HostileAI AttachedAI;

    Plane[] CamPlanes;

	void Start () {
        Debug.Log("Got cam" + LightCam.name);
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        AttachedAI = GetComponent<HostileAI>();
    }

    void Update()
    {
        
        CamPlanes = GeometryUtility.CalculateFrustumPlanes(LightCam);

        if (GeometryUtility.TestPlanesAABB(CamPlanes, Player.bounds) && Player.gameObject.activeInHierarchy && AttachedAI.PLSeen)// Checks if player is in LOS
        {
            if (Player.GetComponent<PlayerMovement>().lit)
            {
                AttachedAI.detected = true;
                AttachedAI.aware = true;
                AttachedAI.knownPos = Player.transform.position;
            }
        }
    }
}
