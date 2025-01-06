using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public Transform player;
    public float optionalXOffset = 0f;
    void Update(){
        float targetX = player.transform.position.x;
       
   
        transform.position = new Vector2(targetX + optionalXOffset, transform.position.y);
    }
}
