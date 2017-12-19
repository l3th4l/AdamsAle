using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour {

	public Elevator El;

	void OnTriggerEnter(Collider Player)
	{
        print(Player.tag);
		if (Player.CompareTag ("Player"))
			El.onElev = true;
	}	
	void OnTriggerExit(Collider Player)
	{
		if (Player.CompareTag ("Player"))
			El.onElev = false;
	}
}
