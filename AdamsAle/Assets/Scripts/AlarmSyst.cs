using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSyst : MonoBehaviour {
    public bool Alert;
    public float AlertTime;
    public float MaxAlertTime;

    void Start ()
    {
        AlertTime = 0.0f;
	}

	void Update ()
    {
        Collider[] EntInRad = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);

        if (Alert)
        {
            foreach (Collider en in EntInRad)
            {
                if (en.CompareTag("Turret"))
                    en.gameObject.GetComponent<Turret>().enabled = true;

                if (en.CompareTag("AI"))
                {
                    AI enAI = en.gameObject.GetComponent<AI>();
                    enAI.Alarmed = true;
                }

                print(en.name);
            }
        }
	}
}
