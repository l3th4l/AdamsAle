using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideTrigger : MonoBehaviour {

    public Image hideImage;
    public Transform player;
    public KeyCode hideKeycode;

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            hideImage.enabled = true;

            if(Input.GetKeyDown(KeyCode.F))
            {
                player.transform.Translate(player.transform.position.x, player.transform.position.y, 5f);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            hideImage.enabled = false;
        }
    }
}
