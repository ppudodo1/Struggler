using UnityEngine;

public class GrenadeController : MonoBehaviour
{

    private float timeToLive = 5f;
    private Enemy enemy;
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

            enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(50);

            Destroy(gameObject);
        }


    }
}
