using UnityEngine;
using System;

public class SkeletonAI : MonoBehaviour
{
   public GameObject player;
   private float playerX;
   private float speed = 1.5f;
   private float jumpingPower = 5f;
   private BoxCollider2D obstacleCollider;
   [SerializeField] private Rigidbody2D rb;
    private SpriteRenderer m_SpriteRenderer;
    private Vector3 position;


    void Start()
    {
        obstacleCollider = GetComponents<BoxCollider2D>()[1];
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        position = player.transform.position;
        playerX = position.x;
        Debug.Log(playerX);
    }

    
    void Update()
    {
        position = player.transform.position;
        playerX = position.x;

        if (transform.position.x > playerX){
            m_SpriteRenderer.flipX = false;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            obstacleCollider.offset = new Vector2(-1f,0f); 

        }

        else if (transform.position.x < playerX){
            m_SpriteRenderer.flipX = true;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            obstacleCollider.offset = new Vector2(1f,0f); 


        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Ground"))
        { 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }
        else if(collision.gameObject.CompareTag("Player")){
            UnityEditor.EditorApplication.isPlaying = false;

        }
            

    }
}
