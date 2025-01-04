using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public Transform player; 
    public float followSpeed = 2.0f; 
    public float yPos = -1.5f;
    void Update()
    {
        float targetX = player.transform.position.x;
       
   
        transform.position = new Vector2(targetX, yPos);
    }
}
