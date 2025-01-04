using UnityEngine;
using System.Collections;
public class GrenadeController : MonoBehaviour
{

    private float timeToLive = 5f;
    private Enemy enemy;
    private float pushBackForce = -1f;
    private float jumpingPower = 2f;

    void Start(){


    
    }

    void Update(){
        
        timeToLive -= Time.deltaTime;
        if(timeToLive < 0) Destroy(gameObject);

    }


    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Ground")){
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Enemy")){
            
            Destroy(gameObject);

            enemy = collision.GetComponent<Enemy>();

            if(enemy != null){
                enemy.TakeDamage(25);
                enemy.StartColorChange();
                PushBackEnemy(collision);

            }
            



        }
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
    // Ovdje je isto bug kad ubijes enemya da svejedno trazi unisteni enemy game object
/*
    private IEnumerator ChangeEnemyColor(SpriteRenderer enemySpriteRenderer){
        Color enemyDefaultColor = enemySpriteRenderer.color;
        enemySpriteRenderer.color = new Color(1f, 0.8f, 0.8f, 1f);

        yield return new WaitForSeconds(0.5f);

        if (enemySpriteRenderer != null){
        enemySpriteRenderer.color = enemyDefaultColor;
    }

    }
    */
}
