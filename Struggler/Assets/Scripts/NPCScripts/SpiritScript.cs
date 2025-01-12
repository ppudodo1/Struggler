using UnityEngine;
using System.Collections;
public class SpiritScript : MonoBehaviour
{
    public Transform posA, posB;
    public int speed;
    Vector2 targetPos;
    //private bool isFacingRight = true;
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded => Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    void Start()
    {
        targetPos = posB.position;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < .1f)
        {
            targetPos = posB.position;
            if (transform.localScale.x < 0f) {
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
        if (isGrounded)
        {
            StartCoroutine(DelayedJump());
        }

        //rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5f);
    }
    private IEnumerator DelayedJump() {
        yield return new WaitForSeconds(0.2f);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 5f);
    }
   /*private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        }
    }*/
}
