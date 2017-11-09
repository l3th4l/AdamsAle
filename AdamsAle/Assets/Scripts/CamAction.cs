using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAction : MonoBehaviour {
    Collider PlayerCol;
    SecurityCam AttachedMovement;
    GameObject Player;
    public GameObject Alarm;

	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerCol = Player.GetComponent<Collider>();
        AttachedMovement = GetComponentInParent<SecurityCam>();
	}

    void FixedUpdate()
    {

        if (Player.activeInHierarchy)
        {
            Plane[] CamPl;
            CamPl = GeometryUtility.CalculateFrustumPlanes(this.GetComponent<Camera>());
            if (GeometryUtility.TestPlanesAABB(CamPl, PlayerCol.bounds))
            {
                AttachedMovement.enabled = false;
                AttachedMovement.Tm = (Mathf.Sin(AttachedMovement.Tm) * 90 > 0) ? 90 : 180;
                Alarm.GetComponent<AlarmSyst>().Alert = true;
            }
            else
            {
                AttachedMovement.enabled = true;
            }
        }else
        {
            print("No player");
        }
    }
}
