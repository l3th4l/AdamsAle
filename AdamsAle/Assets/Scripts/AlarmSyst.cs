using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSyst : MonoBehaviour {
    public bool Alert;
    public float AlertTime;
    public float MaxAlertTime;

    GameObject Player;

    void Start ()
    {
        AlertTime = 0.0f;
        Player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
    {
        bool inZone = false;
        Collider[] EntInRad = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size / 2, transform.rotation);

        foreach (Collider en in EntInRad)
        { 
            if (en.CompareTag("Player"))
                inZone = true;
        }
        if(inZone)
        {
            foreach (Collider en in EntInRad)
            {
                if (en.CompareTag("AI"))
                    en.GetComponent<AI>().inZone = true;
            }
        }
        else
        {
            foreach (Collider en in EntInRad)
            {
                if (en.CompareTag("AI"))
                    en.GetComponent<AI>().inZone = false;
            }
        }

        if (Alert)
        {
            foreach (Collider en in EntInRad)
            {

                if (Player.activeInHierarchy)
                {
                    if (en.CompareTag("Turret"))
                        en.gameObject.GetComponent<Turret>().enabled = AlertTime <= MaxAlertTime;

                    if (en.CompareTag("Alarm"))
                    {
                        if (!en.GetComponent<AlarmSyst>().Alert && AlertTime <= 0.0f)
                            en.GetComponent<AlarmSyst>().Alert = true;
                    }

                    if (en.CompareTag("AI"))
                    {
                        print(AlertTime <= MaxAlertTime * 0.01 + (Time.deltaTime + Time.fixedDeltaTime));
                        en.gameObject.GetComponent<AI>().Alarmed = AlertTime <= MaxAlertTime * 0.01 + (Time.deltaTime + Time.fixedDeltaTime);
                    }
                }
                AlertTime += Time.deltaTime;
                if (AlertTime >= MaxAlertTime)
                {
                    Alert = false;
                    AlertTime = 0.0f;
                }
            }
        }
        else
        {
            foreach (Collider en in EntInRad)
            {
                if (en.CompareTag("AI"))
                    en.gameObject.GetComponent<AI>().Alarmed = false;
            }
        }
	}
}
