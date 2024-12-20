using UnityEngine;

public class PlatformControllerž : MonoBehaviour
{
   
    public Transform posA, posB;
    public int speed;
    Vector2 targetPos;
    void Start()
    {
        targetPos = posB.position;
    }

   
    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < .1f) {
            targetPos = posB.position;
        }
        if (Vector2.Distance(transform.position, posB.position) < .1f)
        {
            targetPos = posA.position;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // Parent the player to the platform
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null); // Detach the player from the platform
        }
    }
}
