using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMelee : MonoBehaviour {

    public float meleeRange = 3f;
    public KeyCode meleeKeycode = KeyCode.E;
    private Animator anim;
	public int MeleeDamage;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, transform.right, out _hit, meleeRange))
        {
            Enemy enemy = _hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {

                if(Input.GetKeyDown(meleeKeycode))
                {
                    anim.SetTrigger("Melee");
					enemy.TakeDamage(MeleeDamage);
                }
            }

        }

    }
}
