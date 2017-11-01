using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
	public Collider ElevTrigger;
	Vector3 pos;
	public Transform target;
	Vector3 pos2;
	public bool onElev;
	Rigidbody rb;
	int lp;
	Vector3 vel;
	public float Velocity;
	// Use this for initialization
	void Start () {
		vel = Vector3.zero;
		pos = transform.position;
		pos2 = target.transform.position;
		rb = GetComponent<Rigidbody> ();
		lp = 1;
		//print (lp);
		Velocity = Velocity / 100;
	}

	void Update () {
		transform.Translate(vel);
		if(onElev){
			if (Input.GetKeyDown (KeyCode.Q)) {
				if (lp == 1 ) {
					//print ("Go up nibba");
					vel = (pos2 - pos).normalized * Velocity;
				}
				if (lp == -1) {
					//print ("Go down nibba");
					vel = (pos - pos2).normalized * Velocity;
				}
			} else {
				//print (lp + ";" + up);
			}
		}
		if (lp == 1 && vel == (pos - pos2).normalized * Velocity ) {
			//print ("Staaapppphhhhh");
			vel = Vector3.zero;
			//print ("KYS");
		}
		if (lp == -1 && vel == (pos2 - pos).normalized * Velocity ) {
			//print ("Staaapppphhhhhhhh!!!!!!!!!!!!!");
			vel = Vector3.zero;
			//print ("Nigga");
		}
		//print (lp);
			
	}
	void OnTriggerStay(Collider Ends)
	{
		//print (1);
	}
	void OnTriggerEnter(Collider Ends)
	{
		//print (2);
		//print(Ends.tag);
		if (Ends.CompareTag ("lp"))
			lp = 1;
		else 
			if (Ends.CompareTag ("up"))
				lp = -1;
			else
				lp = 0;
	}
	void OnTriggerExit(Collider Ends)
	{
		if (Ends.CompareTag ("lp") || Ends.CompareTag ("up")) {
			lp = 0;
		}
	}
}

