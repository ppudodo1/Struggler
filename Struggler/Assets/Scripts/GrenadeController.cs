using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    void Start(){
        


    }

    void Update(){
        
        

    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Ground") || collision.CompareTag("Enemy")){
            Destroy(gameObject);
        }


    }
}
