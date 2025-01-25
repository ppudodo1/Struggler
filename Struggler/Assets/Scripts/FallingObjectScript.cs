using UnityEngine;

public class FallingObjectScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public int gravtiyMulitiplier = 1;
    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody2D>();

        // Disable gravity at the start
        if (rb != null)
        {
           // rb.useGravity = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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
}
