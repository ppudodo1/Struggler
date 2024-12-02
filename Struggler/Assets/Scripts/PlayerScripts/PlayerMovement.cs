using UnityEngine;
using System;
using System.Collections;
public class PlayerMovement:MonoBehaviour
{
    private float horizontal;
    private float speed = 5f;

    public float jumpingPower = 4f;
    public float pushBackForce = -5f;

    private bool isFacingRight = true;
    private bool hasSecondJump = true;

    //ima neki overlap sa fixed update pa ga nebi pravilno "gurnulo"
    private bool isBeingPushed = false;

    private SpriteRenderer spriteRenderer;

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
   
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
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

            audioSource.PlayOneShot(dashSound);

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


    private void FixedUpdate(){
        if(!isBeingPushed)
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
        if (collision.gameObject.CompareTag("Ground")){ 
            audioSource.PlayOneShot(landingSound);
            hasSecondJump = true;
            gameObject.GetComponent<Animator>().SetBool("isJumping", false);
        }


        else if(collision.gameObject.CompareTag("Enemy")){

            healthSystem.removeHeart();

            if (collision.gameObject.transform.position.x > transform.position.x){
                StartCoroutine(PushBack(false));
            }

            else{
            StartCoroutine(PushBack(true)); 
            }
            
        }
        else if(collision.gameObject.CompareTag("Heal")){
            healthSystem.addHeart();
            Destroy(collision.gameObject);
        }
   
    //kada primi damage
    IEnumerator PushBack(bool pushRight){


        isBeingPushed = true; 
        Color currentColor = spriteRenderer.color;


        if(defaultColor == spriteRenderer.color){
            spriteRenderer.color = new Color(currentColor.r + 0.5f, currentColor.g * 0.7f,currentColor.b * 0.7f);

            // da ne playa vise hurt zvukova u isto vrime
            audioSource.PlayOneShot(hurtSound, 0.3f);
        }

        gameObject.GetComponent<Animator>().SetBool("isJumping", true);
        if (!pushRight){

            rb.linearVelocity = new Vector2(pushBackForce, jumpingPower * 0.7f);

        }

        else{

            rb.linearVelocity = new Vector2(-pushBackForce, jumpingPower * 0.7f);

        }

        yield return new WaitForSeconds(0.5f);

        spriteRenderer.color = defaultColor;
        isBeingPushed = false; 
    }

    }
}
