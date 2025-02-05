using UnityEngine;

public class FallingObjectScript : MonoBehaviour
{
    private Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    public float distance;
    public int gravtiyMulitiplier = 1;
    bool isFalling = false;
    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        
    }
    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if (isFalling == false)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, distance);
            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.CompareTag("Player"))
                {  
                    rb.gravityScale = 5;
                    isFalling = true;
                    break; 
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


    /* private void OnTriggerEnter2D(Collider2D other)
     {
         // Check if the player triggered the collider
         //Debug.Log("Collision");
         if (other.CompareTag("Player"))
         {
             rb.gravityScale = gravtiyMulitiplier;
             // Enable gravity when triggered
             //Vector3 customGravity = Physics.gravity * 1.0f;
             //rb.AddForce(customGravity, ForceMode.Acceleration);
         }
     }
     private void OnCollisionEnter2D(Collision2D collision)
     {
         //GetComponent<>
     }*/
}
