using UnityEngine;

public class CameraFollow:MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    
     void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x,player.position.y - 0.5f + offset.y, offset.z);
    }
}
