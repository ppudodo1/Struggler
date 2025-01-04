using UnityEngine;

public class FollowPlayerXSmooth : MonoBehaviour
{
    public Transform player; 
    public float followSpeed = 2.0f; 

    void Update()
    {
        float targetX = player.transform.position.x;
        float smoothX = Mathf.Lerp(transform.position.x, targetX, followSpeed * Time.deltaTime);
   
        transform.position = new Vector2(smoothX, transform.position.y);
    }
}
