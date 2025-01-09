using UnityEngine;

public class CameraFollow:MonoBehaviour
{
    public Transform player;
    private bool touchedBarrier = false;
    public Vector3 offset;
    
     void Update()
    {
        if(!touchedBarrier){
        transform.position = new Vector3(
             player.position.x + offset.x, 
             player.position.y + offset.y, 
             offset.z                     
         );
        }


    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Barrier")){
            touchedBarrier = true;
            transform.position = new Vector2 (other.transform.position.x, player.position.y + offset.y);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Barrier")){
            touchedBarrier = false;
        }
    }
}
