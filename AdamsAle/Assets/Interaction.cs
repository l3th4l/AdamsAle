using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

    public bool Attach_toPlayer;
    public bool pickedUp = false;

    private GameObject player;

    private bool playerInTrigger;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update ()
    {
        if (pickedUp)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
            if (Attach_toPlayer)
                transform.position = player.transform.position;
        }
        if(playerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
                pickedUp = !pickedUp;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInTrigger = false;
    }
}
