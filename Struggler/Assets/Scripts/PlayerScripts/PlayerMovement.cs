using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 5f;

    public float invincibilityFrames = 1f;
    private float invincibilityTimer;
    private bool playerCanTakeDmg = true;

    public float jumpingPower = 4f;
    public float pushBackForce = -5f;

    private bool isFacingRight = true;
    private bool hasSecondJump = true;

    private bool isBeingPushed = false;

    private bool isOnMovingPlatform = false;
    private GameObject movingPlatform;

    private SpriteRenderer spriteRenderer;
    private bool wasGrounded;
    private Color defaultColor;
    public HealthSystem healthSystem;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landingSound;

    private AudioSource audioSource;

    private Animator animator;

    private bool isGrounded => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    private void Start()
    {
        invincibilityTimer = invincibilityFrames;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        defaultColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (!playerCanTakeDmg)
        {
            invincibilityTimer -= Time.deltaTime;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

     
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump(false);
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded && hasSecondJump)
        {
            Jump(true);
        }
       


        Flip();

        animator.SetFloat("MoveValue", Mathf.Abs(horizontal));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);

     
        if (isGrounded && !wasGrounded) 
        {
            Land();
        }

        wasGrounded = isGrounded;
    }

    private void FixedUpdate()
    {
        if (!isBeingPushed)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    private void Jump(bool isDoubleJump)
    {
        audioSource.PlayOneShot(isDoubleJump ? dashSound : jumpSound);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower + (isDoubleJump ? 3f : 0f));
        animator.SetBool("isJumping", true);

        if (isDoubleJump)
        {
            hasSecondJump = false;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Land();
        }
        else if (collision.CompareTag("MovingPlatform"))
        {
            movingPlatform = collision.gameObject;
            isOnMovingPlatform = true;
            Land();
        }
        else if (collision.CompareTag("Heal"))
        {
            healthSystem.addHeart();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            isOnMovingPlatform = false;
        }
    }

    private void Land()
    {
        animator.SetBool("isJumping", false);
        hasSecondJump = true;
        audioSource.PlayOneShot(landingSound);
    }
}
