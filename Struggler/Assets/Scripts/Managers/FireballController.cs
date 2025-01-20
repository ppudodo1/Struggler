using UnityEngine;

public class FireballController : MonoBehaviour
{
    private Enemy enemy;
    private float pushBackForce = -1f;
    private float jumpingPower = 2f;

    
    void Start(){


    
    }

    void Update(){
        

    }


    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Enemy")){
            
            Destroy(gameObject);

            enemy = collision.GetComponent<Enemy>();

            if(enemy != null){
                enemy.TakeDamage(25);
                enemy.StartColorChange();
                PushBackEnemy(collision);

            }
        
        }
        else if(collision.CompareTag("Ground")){
            Destroy(gameObject);

        }
        else if(collision.CompareTag("EnemyProjectile")){
            Destroy(gameObject);
        }

        /* BRB
        else if(collision.CompareTag("Player")){

            Vector2 tempVector = transform.position;

            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            HealthSystem hp = collision.GetComponent<HealthSystem>();

            player.playerCanTakeDmg = false;
            player.healthSystem.removeHeart();
            if (collision.gameObject.transform.position.x > tempVector.x)
            {
                StartCoroutine(player.PushBack(true));
            }

            else
            {
                StartCoroutine(player.PushBack(false));
            }
            

        }
        */
      
    }
    

    private void PushBackEnemy(Collider2D collision){
        SpriteRenderer enemySpriteRenderer = collision.GetComponent<SpriteRenderer>();

        Transform enemyTransform = collision.transform;
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if(rb == null){
            Debug.Log("No RB found!");

            return;
        }
        
        if (enemyTransform.position.x < transform.position.x){

            rb.linearVelocity = new Vector2(pushBackForce, jumpingPower);

        }

        else{

            rb.linearVelocity = new Vector2(-pushBackForce, jumpingPower);

        }
    }
}
