using UnityEngine;
using System;
public class ThrowerAI : MonoBehaviour
{
    public GameObject player;

    private GameObject grenade;
    private GameObject instantiatedObject;
    
    private float playerX;
    public float activationDistance = 8f;
    public float speed = 5f;

    private bool movingRight = true;
    public float switchSidestimer = 2f;
    private float defaultSwitchSidesTimer;
    private Rigidbody2D grenadeRb;


    private float attackTimer;

    private Rigidbody2D rb;
    private SpriteRenderer m_SpriteRenderer;
    private Vector3 position;
    void Start()
    {
        grenade = Resources.Load<GameObject>("Prefabs/EnemyProjectile");
        defaultSwitchSidesTimer = switchSidestimer;
        attackTimer = UnityEngine.Random.Range(0.5f,3f);

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        position = player.transform.position;
        playerX = position.x;
        Debug.Log(playerX);
    }
    
    void Update()
    {
        switchSidestimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;


        if(switchSidestimer < 0f){
            switchSidestimer = defaultSwitchSidesTimer;

            int randomNum = UnityEngine.Random.Range(1,3);


            if (randomNum == 1)
                movingRight = true;
            else
                movingRight = false;

        }
    

        if(attackTimer < 0f){
           
            attackTimer = UnityEngine.Random.Range(1f,3f);

            if(m_SpriteRenderer.flipX){

                instantiatedObject = Instantiate(grenade, new Vector2(transform.position.x - 1.5f,transform.position.y), Quaternion.Euler(0, 0, 0));
                grenadeRb = instantiatedObject.GetComponent<Rigidbody2D>();

                grenadeRb.AddForce(new Vector2(UnityEngine.Random.Range(-50, -30), UnityEngine.Random.Range(5, 10)), ForceMode2D.Impulse);
            }

            else if(!m_SpriteRenderer.flipX){
                instantiatedObject = Instantiate(grenade, new Vector2(transform.position.x + 1.5f,transform.position.y), Quaternion.Euler(0, 0, 0));
                grenadeRb = instantiatedObject.GetComponent<Rigidbody2D>();

                grenadeRb.AddForce(new Vector2(UnityEngine.Random.Range(30,60),UnityEngine.Random.Range(5,10)), ForceMode2D.Impulse);
            }

            grenadeRb.AddTorque(-2, ForceMode2D.Impulse);

        }
    


        position = player.transform.position;
        playerX = position.x;

        if (transform.position.x > playerX && Math.Abs(transform.position.x - playerX) < activationDistance){
            m_SpriteRenderer.flipX = true;
        
        }
        else if (transform.position.x < playerX && Math.Abs(transform.position.x - playerX) < activationDistance){
            m_SpriteRenderer.flipX = false;

        }
    }

    void FixedUpdate()
    {
        if(movingRight)
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        else 
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("InvisibleWall")){
            switchSidestimer = defaultSwitchSidesTimer;
            if(movingRight) movingRight = false;
            else movingRight = true;
        }
    }
}
