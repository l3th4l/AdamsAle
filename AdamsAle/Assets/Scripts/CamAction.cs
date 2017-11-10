using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAction : MonoBehaviour {
    Plane[] CamPl;
    Collider PlayerCol;
    SecurityCam AttachedMovement;
<<<<<<< HEAD
<<<<<<< HEAD
    GameObject Player;
=======
>>>>>>> parent of 46063d9... Bug fixes
    public GameObject Alarm;
=======
>>>>>>> parent of edf14d7... Added a FOV Visualizer to the security cam

	void Start ()
    {
        PlayerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        AttachedMovement = GetComponentInParent<SecurityCam>();
	}

	void Update ()
    {
        print(Time.fixedDeltaTime);
        CamPl = GeometryUtility.CalculateFrustumPlanes(this.GetComponent<Camera>());
        if(GeometryUtility.TestPlanesAABB(CamPl,PlayerCol.bounds))
        {
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
            AttachedMovement.enabled = false;
            AttachedMovement.Tm = (Mathf.Sin(AttachedMovement.Tm) * 90 > 0) ? 90 : 180;
        }
        else
>>>>>>> parent of edf14d7... Added a FOV Visualizer to the security cam
=======
            AttachedMovement.enabled = false;
            AttachedMovement.Tm = (Mathf.Sin(AttachedMovement.Tm) * 90 > 0) ? 90 : 180;
            Alarm.GetComponent<AlarmSyst>().Alert = true;
        }
        else
>>>>>>> parent of 46063d9... Bug fixes
        {
            AttachedMovement.enabled = true;
        }
	}
}
