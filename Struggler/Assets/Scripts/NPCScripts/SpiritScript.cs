using UnityEngine;

public class SpiritScript : MonoBehaviour
{
    public Transform posA, posB;
    public int speed;
    Vector2 targetPos;
    private bool isFacingRight = true;
    void Start()
    {
        targetPos = posB.position;
    }

    
    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < .1f)
        {
            targetPos = posB.position;
            if (transform.localScale.x<0f) {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;

            }
        }
        if (Vector2.Distance(transform.position, posB.position) < .1f)
        {
            targetPos = posA.position;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
