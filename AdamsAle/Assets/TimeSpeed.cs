using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeed : MonoBehaviour {

    public float TS = 10;
    	
	// Update is called once per frame
	void Update () {
        Time.timeScale = TS / 10;
	}
}
