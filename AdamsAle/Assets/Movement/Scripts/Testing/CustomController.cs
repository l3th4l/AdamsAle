namespace Movement
{
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

        private bool isGrounded, hasJumpQueued;

        [SerializeField]
        private TriggerHook2D groundedHook;

        [SerializeField]
        private float movementSpeed = 5f, jumpSpeed = 10f, jumpCooldown = 0.5f;

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
            if (this.isGrounded && Input.GetButtonDown("Jump"))
                this.hasJumpQueued = true;
        }

        private void FixedUpdate()
        {
            this.SetMovementInput();
            this.UpdateSpriteFlip();
            this.ApplyGravity();
            this.ApplyJumpInput();
            this.Apply();
        }

        private void SetMovementInput()
        {
            if (this.isGrounded)
                this.inputSpeed.x = Input.GetAxis("Horizontal") * this.movementSpeed * Time.deltaTime;
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
                this.velocity += Vector2.up * this.jumpSpeed * Time.deltaTime;
                this.hasJumpQueued = false;
            }
        }

        private void Apply()
        {
            this.rb.MovePosition(this.rb.position + this.inputSpeed + this.velocity);
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