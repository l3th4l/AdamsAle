using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    public float ClimbSpeed;
    [SerializeField]
    bool NotLadder;

    private CharacterController other;
    private bool inTrig;

    Vector3 pos;

    private void Start()
    {
        other = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
    }
    private void Update()
    {
        
        if (inTrig)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //other.transform.position = new Vector3(other.transform.position.x, pos.y, other.transform.position.z) + transform.up * Input.GetAxisRaw("Vertical") * ClimbSpeed;
                if (!NotLadder)
                {
                    other.GetComponent<PlayerMovement>().verticalSpeed = -other.GetComponent<PlayerMovement>().gravity * Time.fixedDeltaTime;
                    other.transform.Translate(Input.GetAxisRaw("Vertical") * ClimbSpeed * transform.up * Time.deltaTime);
                }
                else
                {
                    other.transform.Translate(Input.GetAxisRaw("Vertical") * ClimbSpeed * transform.up * Time.deltaTime * ((other.transform.rotation.y != 0)? -1:1));
                }
            }
            pos = other.transform.position;
        }

    }
    private void OnTriggerEnter(Collider pl)
    {
        if(pl.CompareTag("Player"))
            inTrig = true;
    }
    private void OnTriggerExit(Collider pl)
    {
        if (pl.CompareTag("Player"))
            inTrig = false;
    }
}
