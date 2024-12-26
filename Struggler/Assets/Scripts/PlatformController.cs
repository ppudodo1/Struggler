using UnityEngine;

public class PlatformController : MonoBehaviour
{
   
    public Transform posA, posB;
    public int speed;
    Vector2 targetPos;
    private Vector2 lastPosition;
    void Start()
    {
        targetPos = posB.position;
    }

   
    void Update(){

        if (Vector2.Distance(transform.position, posA.position) < .1f) {
            targetPos = posB.position;
        }
        if (Vector2.Distance(transform.position, posB.position) < .1f)
        {
            targetPos = posA.position;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }



    private void FixedUpdate(){

        Vector2 platformVelocity = ((Vector2)transform.position - lastPosition) / Time.deltaTime;
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
