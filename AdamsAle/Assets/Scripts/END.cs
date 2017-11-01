using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class END : MonoBehaviour {

	public GameObject endCanvas;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			endCanvas.SetActive (true);
		}
	}
}
