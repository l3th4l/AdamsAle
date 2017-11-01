using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerOutro : MonoBehaviour {

	public GameObject outroCanvas;

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			outroCanvas.SetActive (true);
		}
	}
}
