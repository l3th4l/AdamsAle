using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeed : MonoBehaviour {
    public float x = 10;

	void Update () {
        Time.timeScale = x / 10;
	}
}
