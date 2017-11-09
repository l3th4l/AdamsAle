using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOnDeath : MonoBehaviour {

	void Update ()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
            Time.timeScale = 0.2f;
	}
}
