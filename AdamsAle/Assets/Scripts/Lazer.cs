using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {

    public float SwitchTime;
    float PassedTime;
    public GameObject LazObj;
    public GameObject[] Turrets;
	// Use this for initialization
	void Start ()
    {
		PassedTime = Time.fixedTime;
        foreach(GameObject Turr in Turrets)
        {
            Turret Tur_Sam = Turr.GetComponent<Turret>();
            Tur_Sam.enabled = false;
        }
	}
    private void FixedUpdate()
    {
        if((Time.fixedTime - PassedTime) * (Time.fixedTime - PassedTime) >= SwitchTime * SwitchTime)
        {
            LazObj.SetActive(!LazObj.activeInHierarchy);
            PassedTime = Time.time;
        }
        if(LazObj.GetComponent<LazerTrigger>().playerInTrig)
        {
            foreach (GameObject Turr in Turrets)
            {
                Turret Tur_Sam = Turr.GetComponent<Turret>();
                Tur_Sam.enabled = true;
            }
        }
    }
}

