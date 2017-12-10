using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour {

	public Elevator El;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider Player)
	{
		if (Player.CompareTag ("Player"))
			El.onElev = true;
	}	
	void OnTriggerExit(Collider Player)
	{
		if (Player.CompareTag ("Player"))
			El.onElev = false;
	}
}
