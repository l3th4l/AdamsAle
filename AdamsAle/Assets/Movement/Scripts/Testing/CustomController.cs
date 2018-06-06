namespace Movement
{
    using System;
    using CollisionUtility;
    using UnityEngine;

    [RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(SpriteRenderer))]
    internal sealed class CustomController : MonoBehaviour
    {
        private Animator anim;
        private Rigidbody2D rb;
        private SpriteRenderer rend;

        private Vector2
            inputSpeed,
            velocity;
        
        private bool
            isGrounded,
            isSprinting,
            hasJumpQueued;

        private Vector2 lastPosition;

        [SerializeField]
        private TriggerHook2D groundedHook;

        [SerializeField]
        private float
            walkSpeed = 5f,
            sprintSpeed = 8f,
            jumpSpeed = 10f;

        [SerializeField]
        private float
            jumpCooldown = 0.5f;

        private void Reset()
        {
            this.groundedHook = this.GetComponentInChildren<TriggerHook2D>();
        }

        private void Awake()
        {
            this.anim = this.GetComponent<Animator>();
            this.rb = this.GetComponent<Rigidbody2D>();
            this.rend = this.GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            this.groundedHook.Stayed2D += this.OnGroundStay;
            this.groundedHook.Exit2D += this.OnGroundExit;
        }
        
        private void OnDestroy()
        {
            this.groundedHook.Stayed2D -= this.OnGroundStay;
            this.groundedHook.Exit2D -= this.OnGroundExit;
        }

        private void Update()
        {
            this.UpdateInput();
        }

        private void FixedUpdate()
        {
            this.SetMovementInput();
            this.UpdateSpriteFlip();
            this.ApplyGravity();
            this.ApplyJumpInput();
            this.Apply();
            this.UpdateAnimatorSpeed();
        }

        private void Jump()
        {
            this.velocity += Vector2.up * this.jumpSpeed * Time.deltaTime;
            this.hasJumpQueued = false;

            this.anim.SetTrigger("Jump");
        }

        private void UpdateInput()
        {
            if (this.isGrounded && Input.GetButtonDown("Jump"))
                this.hasJumpQueued = true;

            this.isSprinting = Input.GetButton("Sprint");
        }

        private void SetMovementInput()
        {
            if (this.isGrounded)
                this.inputSpeed.x =
                    Input.GetAxis("Horizontal")
                    * (this.isSprinting ? this.sprintSpeed : this.walkSpeed)
                    * Time.deltaTime;
        }

        private void UpdateSpriteFlip()
        {
            if (this.inputSpeed.x != 0f)
                this.rend.flipX = Mathf.Sign(this.inputSpeed.x) == -1f;
        }

        private void ApplyGravity()
        {
            this.velocity += Physics2D.gravity * Time.deltaTime * Time.deltaTime;
        }

        private void ApplyJumpInput()
        {
            if (this.isGrounded && this.hasJumpQueued)
            {
                Jump();
            }
        }

        private void Apply()
        {
            this.rb.MovePosition(this.rb.position + this.inputSpeed + this.velocity);
        }

        private void UpdateAnimatorSpeed()
        {
            float measuredSpeed =
                Vector2.Distance(this.lastPosition, this.rb.position)
                * (1f / Time.deltaTime);
            this.anim.SetFloat("Speed", measuredSpeed);
            this.lastPosition = this.rb.position;
        }

        // This executes after FixedUpdate
        private void OnGroundStay(Collider2D other)
        {
            this.isGrounded = true;
            this.velocity = Vector2.zero;
        }

        // This executes after FixedUpdate
        private void OnGroundExit(Collider2D other)
        {
            this.isGrounded = false;
        }
    }
}