using UnityEngine;

public class PlatformController : MonoBehaviour
{
   
    public Transform posA, posB; // Start and end positions
    public float speed; // Movement speed

    private Vector2 targetPos; // Current target position
    private Vector2 lastPosition; // Last recorded position
    private Rigidbody2D rb;
     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = posB.position;
    }

    void FixedUpdate()
    {
        
        Vector2 currentPosition = rb.position;

        if (Vector2.Distance(currentPosition, posA.position) < 0.1f)
        {
            targetPos = posB.position; 
        }
        if (Vector2.Distance(currentPosition, posB.position) < 0.1f)
        {
            targetPos = posA.position; 
        }

        Vector2 direction = ((Vector2)targetPos - currentPosition).normalized;
        rb.linearVelocity = direction * speed;

        // Update last position for velocity calculation
        Vector2 platformVelocity = (currentPosition - lastPosition) / Time.fixedDeltaTime;
        lastPosition = currentPosition;
    }


    /*
    private void FixedUpdate(){

        Vector2 platformVelocity = ((Vector2)transform.position - lastPosition) / Time.deltaTime;

       // Debug.Log(platformVelocity.x);
        lastPosition = transform.position;

        Collider2D playerCollider = Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0f, LayerMask.GetMask("Player"));

        if (playerCollider != null){
            Rigidbody2D playerRb = playerCollider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(platformVelocity.x, playerRb.linearVelocity.y);
            }
        }
    }
    */



/*
    void OnTriggerStay2D(Collider2D collision){
        if (collision.CompareTag("Player"))
        {
            player.transform.position = new Vector2(transform.position.x, player.transform.position.y);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // Parent the player to the platform
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null); // Detach the player from the platform
        }
    }
    */
}
