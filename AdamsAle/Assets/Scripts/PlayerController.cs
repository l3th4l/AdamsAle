using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    [Header("Movement")]
    [SerializeField]
    float walkSpeed = 3f;
    //[SerializeField]
    //float crouchWalkSpeed = 1.5f;
    [SerializeField]
    float runSpeed = 9f;
	//bool InCover = false;
    //private CapsuleCollider myCollider;
   // private bool isCrouching = false;

    private bool isFacingRight = true;

    [Space]
    [Header("Jump")]
    [SerializeField]
    Transform groundCheckTransform;
    [SerializeField]
    float groundCheckRange = 0.1f;
    [SerializeField]
    float jumpForce = 5f;
    private bool isGrounded;

    
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //myCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
		//InCover = false;
    }
	void FixedUpdate()
	{
	}
    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

       // if(Mathf.Abs(rb.velocity.x) ==0)
       //     animator.SetBool("isCrouching", isCrouching);

        float movement = Input.GetAxisRaw("Horizontal");

        if(Input.GetKey(KeyCode.LeftShift) /*&& !isCrouching */)
        {
            rb.velocity = new Vector3(movement * runSpeed, rb.velocity.y, rb.velocity.z);
        }
        /*else if(isCrouching)
        {
            rb.velocity = new Vector3(movement * crouchWalkSpeed, rb.velocity.y, rb.velocity.z);
        } */
        else
        {
            rb.velocity = new Vector3(movement * walkSpeed, rb.velocity.y, rb.velocity.z);
        }

        RaycastHit hit;
        if(Physics.Raycast(groundCheckTransform.position, Vector3.down, out hit, groundCheckRange))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        

        if(Input.GetKeyDown(KeyCode.Space)  && isGrounded)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(new Vector3(0f, jumpForce*50, 0f));
        }
        
        /*
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = true;
            myCollider.height = 1.5f;
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            isCrouching = false;
            myCollider.height = 2.23f;
        }
        */
        
        if(movement > 0f && !isFacingRight)
        {
            Flip();
        }

        if(movement <0f && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Quaternion rotation = transform.localRotation;
        if(isFacingRight)
        {
            rotation.y = 0f;
        }
        else if(!isFacingRight)
        {
            rotation.y = 180f;
        }

        transform.localRotation = rotation;
    }
	/*void OnTriggerEnter(Collider Cover)
	{
		if (Cover.CompareTag ("CoverCheck"))
			InCover = true;
		else
			InCover = false;        

    }
	void OnTriggerExit(Collider Cover)
	{
		if (Cover.CompareTag ("CoverCheck"))
			InCover = false;
        

    }*/
}
