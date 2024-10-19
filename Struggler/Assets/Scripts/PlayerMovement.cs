using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    public float speed;
    public float jump;
    public float Move;
    public Rigidbody2D rb;
     void Start()
    {
      
    }
     void Update()
    {
        Move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(speed * Move, rb.linearVelocity.y);
    }
}
