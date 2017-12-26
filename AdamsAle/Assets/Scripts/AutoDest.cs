using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDest : MonoBehaviour
{
    float x = 0.0f;
    //public float stayTime;

	
	// Update is called once per frame
	void Update ()
    {
        if (x > 0)
            Destroy(this.gameObject);
	}
    private void LateUpdate()
    {
        x += Time.deltaTime;
    }
}
