using UnityEngine;

public class EdgeColliderManager : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;
    public Vector2 contactPoint;
    public bool isBottomCollision;

    void Start()
    {
        contactPoint = Vector2.zero;
        edgeCollider = GetComponent<EdgeCollider2D>();
        isBottomCollision = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the first contact point
            ContactPoint2D contact = collision.contacts[0];

            // Store the contact point
            contactPoint = contact.point;

           // Debug.Log($"Player collided at world position: {contactPoint} and player's position is {collision.transform.position}");

            // Check the normal of the first contact
            Vector2 contactNormal = contact.normal;

            if (contactNormal.y < 0)
            {
                isBottomCollision = true;
                Debug.Log("Collision is coming from the bottom.");
            }
            else
            {
                isBottomCollision = false;
                Debug.Log("Collision is NOT coming from the bottom.");
            }
        }
    }

}
