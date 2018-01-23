using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoverTrigger : MonoBehaviour {

	[Tooltip("The Z coordinate of player when in cover")]
	public float coverZ;
	public GameObject player;
	bool isInCover = false;

    float PlayerHeight;
    CharacterController PlControl;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlControl = player.GetComponent<CharacterController>();
        PlayerHeight = PlControl.height;
    }

    private void OnTriggerStay(Collider col) {
		if(col.CompareTag("Player"))
		{
			if(Input.GetKey(KeyCode.C))
			{
				isInCover = true;
				player.transform.DOMoveZ(coverZ, Time.deltaTime*4);               

			}
			else
			{
				isInCover = false;
				player.transform.DOMoveZ(0f, Time.deltaTime * 4);
            }
		}
	}
    private void Update()
    {
        if(isInCover)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            PlControl.height = 0.5f * PlayerHeight;
            player.transform.DOMoveY(transform.position.y, Time.deltaTime * 4);
        }
        else
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            if (Input.GetKeyUp(KeyCode.C))
                PlControl.height = PlayerHeight;
        }
    }
    private void OnTriggerExit(Collider col) 
	{
		if(col.CompareTag("Player") /*&& isInCover*/)
        {
            player.transform.DOMoveZ(0f, Time.deltaTime * 4);
            isInCover = false;

            player.GetComponent<PlayerMovement>().enabled = true;
            PlControl.height = PlayerHeight;
        }
	}
}
