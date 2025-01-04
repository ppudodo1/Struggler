using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public Transform player; 
    void Update(){
        float targetX = player.transform.position.x;
       
   
        transform.position = new Vector2(targetX, transform.position.y);
    }
}
