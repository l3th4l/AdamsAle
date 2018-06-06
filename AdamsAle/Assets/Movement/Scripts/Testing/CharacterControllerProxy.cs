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

        private Vector3 velocity, gravity;

        [SerializeField]
        private float
            normalSpeed = 10f,
            sprintSpeed = 20f,
            jumpSpeed = 10f,
            normalMaxDistance = 0.1f;

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
                this.isJumpQueued = true;
        }

        private void FixedUpdate()
        {
            this.velocity = this.controller.velocity;

            this.UpdateNormal();
            this.ApplyMovementInput();
            this.ApplyJumpInput();
            this.ApplyGravity();

            this.controller.Move(velocity * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.transform.position, this.transform.position + this.velocity);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(this.normal);
            
            // Draw these later, can be hard to see
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(this.transform.position, this.gravity * Time.fixedDeltaTime);

            Gizmos.color =
                this.controller && this.controller.isGrounded
                    ? Color.red
                    : Color.yellow;
            Gizmos.DrawLine(this.ray.origin, this.ray.GetPoint(this.normalMaxDistance));
        }

        private void ApplyGravity()
        {
            this.gravity =
                this.controller.isGrounded
                    ? -this.normal.direction * Physics.gravity.magnitude * Time.deltaTime
                    : Physics.gravity * Time.deltaTime;
            
            this.velocity += this.gravity;
        }

        private void UpdateNormal()
        {
            var origin =
                this.controller.transform.TransformPoint(
                    new Vector3(
                        0f,
                        -this.controller.height * 0.5f,
                        0f));
            var direction = -this.controller.transform.up;
            this.ray = new Ray(origin, direction);

            RaycastHit hit;
            this.normal = 
                Physics.Raycast(this.ray, out hit, this.normalMaxDistance)
                    ? new Ray(hit.point, hit.normal)
                    : new Ray();
        }

        private void ApplyJumpInput()
        {
            if (this.isJumpQueued)
            {
                if (this.controller.isGrounded)
                    this.velocity.y += this.AppliedJumpSpeed;

                this.isJumpQueued = false;
            }
        }

        private void ApplyMovementInput()
        {
            float x = Input.GetAxis("Horizontal");
            
            if (!this.controller.isGrounded && !Mathf.Approximately(x, 0f))
                this.velocity.x = x * this.AppliedSpeed;
            else
            {
                var rot = Quaternion.FromToRotation(Physics.gravity, this.gravity);
                var invRot = Quaternion.FromToRotation(this.gravity, Physics.gravity);
                this.velocity = invRot * this.velocity;
                this.velocity.x = x * this.AppliedSpeed;
                this.velocity = rot * this.velocity;
            }
        }
    }
}