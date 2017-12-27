using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float normalSpeed = 7f;
	public float sprintSpeed = 12f;
	public float jumpForce = 16f;
	public float gravity = 1f;


	float verticalSpeed;
	CharacterController controller;
	private Animator animator;
	private bool isFacingRight = true;
	

	private void Start() {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	private void Update() {

        Debug.Log(1/120.0f + ":" + Time.deltaTime*120/100);

		// X movement
		float x = Input.GetAxis("Horizontal");

		animator.SetFloat("Speed", Mathf.Abs(controller.velocity.x));
		
		if(Input.GetKey(KeyCode.LeftShift))
		{
			// Sprinting
			x = x  * sprintSpeed * Time.deltaTime;
		}
		else
		{
			// Normal walking
			x = x * normalSpeed * Time.deltaTime;
		}


		// Jumping
		if(controller.isGrounded)
		{
			verticalSpeed = -gravity * Time.fixedDeltaTime;
			if(Input.GetButtonDown("Jump"))
			{
				animator.SetTrigger("Jump");
				verticalSpeed = jumpForce * Time.fixedDeltaTime;
			}
		}
		else
		{
			verticalSpeed -= gravity * Time.fixedDeltaTime;
		}

		if(x > 0f && !isFacingRight)
        {
            Flip();
        }

        if(x <0f && isFacingRight)
        {
            Flip();
        }

		Vector3 moveDelta = new Vector3(x, verticalSpeed, 0f);

		controller.Move(moveDelta);
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
}
