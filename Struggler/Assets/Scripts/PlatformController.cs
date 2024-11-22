using UnityEngine;

public class PlatformControllerž : MonoBehaviour
{
    // Start is called once befor1e the first execution of Update after the MonoBehaviour is created
    public Transform posA, posB;
    public int speed;
    Vector2 targetPos;
    void Start()
    {
        targetPos = posB.position;
    }

    // Update is called once per frame
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
}
