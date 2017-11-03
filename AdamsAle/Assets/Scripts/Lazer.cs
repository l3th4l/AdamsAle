using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {

    public float SwitchTime;
    float PassedTime;
    public GameObject LazObj;
	// Use this for initialization
	void Start ()
    {
		PassedTime = Time.fixedTime;
	}

    private void FixedUpdate()
    {
        if((Time.fixedTime - PassedTime) * (Time.fixedTime - PassedTime) >= SwitchTime * SwitchTime)
        {
            LazObj.SetActive(!LazObj.activeInHierarchy);
            PassedTime = Time.time;
        }
    }
}

