using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 5f;

    public float invincibilityFrames = 2f;
    private float invincibilityTimer;
    public bool playerCanTakeDmg = true;

    public float jumpingPower = 4f;
    public float pushBackForce = -5f;

    private bool isFacingRight = true;
    private bool hasSecondJump = true;

    private bool isBeingPushed = false;

    private bool isOnMovingPlatform = false;
    private GameObject movingPlatform;
    private float movementOnPlatform = 5f;

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

    private bool isGrounded => Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);

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
        if (!PauseMenu.isPaused)
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
            if (isGrounded && !wasGrounded)
            {
                Land();
            }

            wasGrounded = isGrounded;
        }
    }

    private void FixedUpdate()
    {

        if (!isBeingPushed)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }

        if (isOnMovingPlatform)
        {
            Debug.Log(movingPlatform.GetComponent<Rigidbody2D>().linearVelocity.x);

            Vector2 velocity = rb.linearVelocity;
            if (isOnMovingPlatform && rb.linearVelocity.x < 0) velocity.x = (movingPlatform.GetComponent<Rigidbody2D>().linearVelocity.x) * speed;
            else if (isOnMovingPlatform && rb.linearVelocity.x > 0) velocity.x = (movingPlatform.GetComponent<Rigidbody2D>().linearVelocity.x) * speed;
            else if (isOnMovingPlatform && rb.linearVelocity.x == 0) velocity.x = (movingPlatform.GetComponent<Rigidbody2D>().linearVelocity.x) * speed;
        }
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 1f);
    }*/
    private void Jump(bool isDoubleJump)
    {
        audioSource.PlayOneShot(isDoubleJump ? dashSound : jumpSound);
        animator.SetBool("isJumping", true);


        StartCoroutine(DelayedJump(isDoubleJump));

        if (isDoubleJump)
        {
            hasSecondJump = false;
        }
    }
    private IEnumerator DelayedJump(bool isDoubleJump)
    {

        yield return new WaitForSeconds(0.2f);


        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower + (isDoubleJump ? 3f : 0f));
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

    //VRLO JE BITAN OVAJ playerCanTakeDmg
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Scaffolding"))
        {
            Land();
        }
        else if (collision.CompareTag("MovingPlatform"))
        {
            // Debug.Log("Detected platform!");
            movingPlatform = collision.gameObject;
            isOnMovingPlatform = true;
            Land();
        }
        else if (collision.CompareTag("Heal"))
        {
            healthSystem.addHeart();
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Shield"))
        {
            healthSystem.addShield();
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Enemy") && playerCanTakeDmg || collision.CompareTag("EnemyProjectile") && playerCanTakeDmg || collision.CompareTag("Projectile") && playerCanTakeDmg)
        {
            playerCanTakeDmg = false;
            healthSystem.removeHeart();


            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                StartCoroutine(PushBack(false));
            }

            else
            {
                StartCoroutine(PushBack(true));
            }

            if (collision.CompareTag("EnemyProjectile") || collision.CompareTag("Projectile"))
            {
                Destroy(collision.gameObject);
            }

        }
        else if (collision.CompareTag("Spikes"))
        {
            playerCanTakeDmg = false;
            healthSystem.removeHeart();
            healthSystem.removeHeart();
            healthSystem.removeHeart();


            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                StartCoroutine(PushBack(false));
            }

            else
            {
                StartCoroutine(PushBack(true));
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && playerCanTakeDmg || collision.CompareTag("EnemyProjectile") && playerCanTakeDmg || collision.CompareTag("Projectile") && playerCanTakeDmg)
        {
            playerCanTakeDmg = false;
            healthSystem.removeHeart();


            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                StartCoroutine(PushBack(false));
            }

            else
            {
                StartCoroutine(PushBack(true));
            }

            if (collision.CompareTag("EnemyProjectile") || collision.CompareTag("Projectile"))
            {
                Destroy(collision.gameObject);
            }

        }
        else if (collision.CompareTag("Spikes"))
        {
            playerCanTakeDmg = false;
            healthSystem.removeHeart();
            healthSystem.removeHeart();
            healthSystem.removeHeart();


            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                StartCoroutine(PushBack(false));
            }

            else
            {
                StartCoroutine(PushBack(true));
            }

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


    public IEnumerator PushBack(bool pushRight)
    {


        isBeingPushed = true;
        Color currentColor = spriteRenderer.color;

        audioSource.PlayOneShot(hurtSound, 0.3f);


        if (defaultColor == spriteRenderer.color)
        {
            spriteRenderer.color = new Color(currentColor.r + 0.5f, currentColor.g * 0.7f, currentColor.b * 0.7f);
        }

        gameObject.GetComponent<Animator>().SetBool("isJumping", true);
        if (!pushRight)
        {

            rb.linearVelocity = new Vector2(pushBackForce, jumpingPower * 0.6f);

        }

        else
        {

            rb.linearVelocity = new Vector2(-pushBackForce, jumpingPower * 0.6f);

        }

        yield return new WaitForSeconds(0.5f);

        spriteRenderer.color = defaultColor;
        isBeingPushed = false;

        StartCoroutine(FadingInAnOut());
    }



    public IEnumerator FadingInAnOut()
    {
        while (invincibilityTimer > 0)
        {

            if (spriteRenderer.color.a == 1f)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.6f); // Half transparent
            }
            else
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f); // Fully opaque
            }

            yield return new WaitForSeconds(0.3f);
        }
        playerCanTakeDmg = true;
        invincibilityTimer = invincibilityFrames;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }
}