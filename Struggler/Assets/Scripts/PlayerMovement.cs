using UnityEngine;
using System;
public class PlayerMovement:MonoBehaviour
{
    private float horizontal;
    private float speed = 5f;
    public float jumpingPower = 4f;
    private bool isFacingRight = true;
    private bool hasSecondJump = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioClip jumpSound;
     [SerializeField] private AudioClip landingSound;
    private AudioSource audioSource;
   
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
     void Update()
    {
        
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            audioSource.PlayOneShot(jumpSound);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            gameObject.GetComponent<Animator>().SetBool("isJumping", true);

        }
        //double jump
        else if(Input.GetButtonDown("Jump") && !(IsGrounded()) && hasSecondJump){

            audioSource.PlayOneShot(jumpSound);

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower + 3f);
            gameObject.GetComponent<Animator>().SetBool("isJumping", true);
            hasSecondJump = false;

        }
        
        Flip();
        gameObject.GetComponent<Animator>().SetFloat("MoveValue", Math.Abs(horizontal));
      
        //Debug.Log("Velocity: " + rb.linearVelocity.y);
    }
    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        gameObject.GetComponent<Animator>().SetFloat("yVelocity", rb.linearVelocity.y);
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

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Ground"))
        { 
            audioSource.PlayOneShot(landingSound);
            hasSecondJump = true;
            gameObject.GetComponent<Animator>().SetBool("isJumping", false);
        }
   

    }
}
