using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public Transform TeleportTo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag ("Player")) 
		{
			col.transform.position = TeleportTo.position;
		}
		/*GameObject sampleGO = Sample.gameObject;
		//if (Sample.CompareTag ("Player")) {
			sampleGO.SetActive (false);
			sampleGO.transform.position = TeleportTo.position;
			sampleGO.SetActive (true);
		//}
		*/
	}
}
