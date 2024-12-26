using UnityEngine;
using System;

public class SkeletonAI : MonoBehaviour
{
   public GameObject player;
   private float playerX;
   private float speed = 1.5f;
   public float jumpingPower = 4f;
    public float activationDistance = 8f;

   [SerializeField] private Rigidbody2D rb;
    private SpriteRenderer m_SpriteRenderer;
    private Vector3 position;

    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;



    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        position = player.transform.position;
        playerX = position.x;
        Debug.Log(playerX);
    }
    
    void Update()
    {
        position = player.transform.position;
        playerX = position.x;

        if (transform.position.x > playerX && Math.Abs(transform.position.x - playerX) < activationDistance){
            m_SpriteRenderer.flipX = false;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        
        }
        else if (transform.position.x < playerX && Math.Abs(transform.position.x - playerX) < activationDistance){
            m_SpriteRenderer.flipX = true;
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        }


        if(m_SpriteRenderer.flipX){
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y - 0.5f), Vector2.right, groundCheckDistance, groundLayer);
                if (hit.collider != null){
                    Jump();
                }
            hit = new RaycastHit2D();
        }
        else if(!m_SpriteRenderer.flipX){
             RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y - 0.5f), Vector2.left, groundCheckDistance, groundLayer);
                if (hit.collider != null){
                    Jump();
                }
            hit = new RaycastHit2D();
        }
    }


    private void Jump(){
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        

    }




}
