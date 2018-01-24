using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //[HideInInspector]
    public bool lit = false;

	public float normalSpeed = 7f;
	public float sprintSpeed = 12f;
	public float jumpForce = 16f;
	public float gravity = 1f;

    float InitHeight;

    public KeyCode Crouch;

	public float verticalSpeed;
	CharacterController controller;
	private Animator animator;
	private bool isFacingRight = true;

    public bool sprinting = false;

    bool Pulling;

    private void Start() {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
        sprinting = false;

        InitHeight = controller.height;
	}

	private void Update() {

        Pulling = false; // reset


		// X movement
		float x = Input.GetAxis("Horizontal");

		animator.SetFloat("Speed", Mathf.Abs(controller.velocity.x));
		
		if(Input.GetKey(KeyCode.LeftShift))
		{
			// Sprinting
			x = x  * sprintSpeed * Time.deltaTime;
            sprinting = true && x!= 0;
		}
		else
		{
			// Normal walking
			x = x * normalSpeed * Time.deltaTime;
            sprinting = false;
		}


        // Push/pull//////////////////
        RaycastHit _PHit;
        if (Physics.Raycast(transform.position, transform.right, out _PHit, armsLength, armsMask))
        {

            movableObj MovObj;

            MovObj = _PHit.transform.GetComponent<movableObj>();

            if (MovObj != null)
            {
                if (Input.GetKey(PushPull))
                {
                    MovObj.push(x * Vector3.right * MovObj.speed);
                    Pulling = true;
                }
            }
        }/////////////////////////////

        // Jumping
        if (controller.isGrounded)
		{
			verticalSpeed = -gravity * Time.fixedDeltaTime;
			if(Input.GetButtonDown("Jump"))
			{
				animator.SetTrigger("Jump");
				verticalSpeed = jumpForce * Time.fixedDeltaTime;
			}

            ///////////Temp crouch
            if (Input.GetKey(Crouch))
                controller.height = InitHeight / 2;
            else
                controller.height = InitHeight;
            ///////////

		}
		else
		{
			verticalSpeed -= gravity * Time.fixedDeltaTime;
		}

		if(x > 0f && !isFacingRight && !Pulling)
        {
            Flip();
        }

        if(x <0f && isFacingRight && !Pulling)
        {
            Flip();
        }

        Vector3 moveDelta = new Vector3(x * ((Pulling) ? _PHit.transform.GetComponent<movableObj>().speed : 1), verticalSpeed, 0f);

		controller.Move(moveDelta);

	}
    public float armsLength;
    public LayerMask armsMask;
    public KeyCode PushPull;

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
}
