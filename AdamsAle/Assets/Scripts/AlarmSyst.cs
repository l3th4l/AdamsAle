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
        print("S: " + GetComponent<BoxCollider>().size + "S/2: " + GetComponent<BoxCollider>().size / 2);
        Collider[] EntInRad = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size / 6, transform.rotation);

        if (Alert)
        {
            foreach (Collider en in EntInRad)
            {
                if (en.CompareTag("Turret"))
                    en.gameObject.GetComponent<Turret>().enabled = AlertTime <= MaxAlertTime;

                if (en.CompareTag("Alarm"))
                {
                    if(!en.GetComponent<AlarmSyst>().Alert && AlertTime <= 0.0f)
                    en.GetComponent<AlarmSyst>().Alert = true;
                }

                if (en.CompareTag("AI"))
                {
                    en.gameObject.GetComponent<AI>().Alarmed = AlertTime <= Time.deltaTime; 
                }
                print(en.tag);
            }
            AlertTime += Time.deltaTime;
            if(AlertTime >= MaxAlertTime)
            {
                Alert = false;
                AlertTime = 0.0f;
            }
        }
	}
}
