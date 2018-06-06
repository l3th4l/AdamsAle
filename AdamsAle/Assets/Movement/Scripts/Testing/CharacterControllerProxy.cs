namespace Movement
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
    internal sealed class CharacterControllerProxy : MonoBehaviour
    {
        private CharacterController controller;

        private bool isJumpQueued;

        private Ray ray, normal;

        private Vector3 gravity;

        [SerializeField]
        private float
            normalSpeed = 10f,
            sprintSpeed = 20f,
            jumpSpeed = 10f,
            gravityNormalDistance = 0.1f;

        private float AppliedSpeed
        {
            get
            {
                return Input.GetButton("Sprint") ? this.sprintSpeed : this.normalSpeed;
            }
        }

        private float AppliedJumpSpeed
        {
            get { return this.jumpSpeed; }
        }

        private void Awake()
        {
            this.controller = this.GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                this.isJumpQueued = true;
            }
        }

        private void FixedUpdate()
        {
            var velocity = this.controller.velocity;
            
            this.ApplyMovementInput(ref velocity);
            this.ApplyJumpInput(ref velocity);
            this.ApplyGravity(ref velocity);

            this.controller.Move(velocity * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(this.normal);
            
            // Draw these later, can be hard to see
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(this.transform.position, this.gravity * Time.fixedDeltaTime);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.ray.origin, this.ray.GetPoint(this.gravityNormalDistance));
        }

        private void ApplyGravity(ref Vector3 velocity)
        {
            var origin = this.controller.transform.TransformPoint(
                new Vector3(
                0f,
                -this.controller.height * 0.5f,
                0f));
            var direction = -this.controller.transform.up;
            this.ray = new Ray(origin, direction);
            float length = this.gravityNormalDistance;
            
            RaycastHit hit;
            if (Physics.Raycast(this.ray, out hit, length))
            {
                this.normal = new Ray(hit.point, hit.normal);
                this.gravity = -this.normal.direction * Physics.gravity.magnitude;
            }
            else
            {
                this.normal = new Ray();
                this.gravity = Physics.gravity;
            }

            // I still don't know why I must multiply by deltaTime.
            velocity += this.gravity * Time.deltaTime;
        }

        private void ApplyJumpInput(ref Vector3 velocity)
        {
            if (this.isJumpQueued)
            {
                if (this.controller.isGrounded)
                {
                    velocity.y += this.AppliedJumpSpeed;
                }

                this.isJumpQueued = false;
            }
        }

        private void ApplyMovementInput(ref Vector3 velocity)
        {
            float x = Input.GetAxis("Horizontal");

            if (this.controller.isGrounded)
            {
                velocity.x = x * this.AppliedSpeed;
            }
            else if (!Mathf.Approximately(x, 0f))
            {
                velocity.x = x * this.AppliedSpeed;
            }
        }
    }
}