using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour {
	
	public Transform CoverPos ;
	public GameObject CoverSprite;
	static GameObject Player;
	bool InCover;
	GameObject CVSpr;


	void Start()
	{
		InCover = false;
		Player = GameObject.FindGameObjectWithTag ("Player");

	}
	void Update()
	{
		if (InCover) {
			if (Input.GetKeyDown (KeyCode.Q)) {
				CVSpr = GameObject.Instantiate (CoverSprite, CoverPos);
				CVSpr.SetActive (true);
				Player.SetActive (false);

			}
			if (Input.GetKeyUp (KeyCode.Q)) {
				Destroy (CVSpr);
				Player.SetActive (true);
			}
		} else {
			if (!Input.GetKey (KeyCode.Q)) {
				Destroy (CVSpr);
				Player.SetActive (true);
			}
		}
	}
	void OnTriggerEnter(Collider Player)
	{
		if (Player.CompareTag ("Player")) {
			InCover = true;
		}
	}
	void OnTriggerExit(Collider Player)
	{
		if (Player.CompareTag ("Player"))
			InCover = false;
		
	}
}
