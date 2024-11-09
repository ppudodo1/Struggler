using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    private float horizontal;
    private float speed = 5f;
    public float jumpingPower = 4f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Reference to the jump sound effect
    [SerializeField] private AudioClip jumpSound;
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();
    }
     void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded()) {

            // Play jump sound
            audioSource.PlayOneShot(jumpSound);

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);  
        }
        Flip();
    }
    // treba rijesiti double jump sada
    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
