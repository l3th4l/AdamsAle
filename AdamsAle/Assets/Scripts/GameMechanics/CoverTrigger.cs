using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoverTrigger : MonoBehaviour {

	[Tooltip("The Z coordinate of player when in cover")]
	public float coverZ;
	public GameObject player;
	bool isInCover = false;

	private void OnTriggerStay(Collider col) {
		if(col.CompareTag("Player"))
		{
			if(Input.GetKey(KeyCode.C))
			{
				isInCover = true;
				player.transform.DOMoveZ(coverZ, 0.8f);
			}
			else
			{
				isInCover = false;
				player.transform.DOMoveZ(0f, 0.8f);
			}
		}
	}

	private void OnTriggerExit(Collider col) 
	{
		if(col.CompareTag("Player") && isInCover)
		{
			player.transform.DOMoveZ(0f, 0.8f);
		}
	}
}
