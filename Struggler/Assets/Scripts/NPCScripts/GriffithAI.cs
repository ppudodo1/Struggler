using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GriffithAI : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private Enemy enemyScript;

    private Vector3 startPosition;
    private bool attackInProgress = false;
    private bool isGrounded = false;

    public Transform[] mapEdges;
    private Transform nextCorner;
    private int jumpCounter = 0;

    public float moveSpeed = 6f;

    public float activationDistance = 10f;

    public float jumpForce = 5f;


   
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<Enemy>();

        Debug.Log("Udaljenost "+Mathf.Abs(transform.position.x - player.transform.position.x));
    }

    void Update()
    {
        Debug.Log(enemyScript.currentHealth);
        if(enemyScript.currentHealth > 1300f)
        {

            if (!attackInProgress && isGrounded)
            {
                Debug.Log("Attack 1");
                attackInProgress = true;
                StartCoroutine(JumpOnPlayer());
            }


        }
        else if(enemyScript.currentHealth > 700f && !attackInProgress)
        {
            attackInProgress = true;
            StartCoroutine(JumpOverPlayer());


        }
        else if(enemyScript.currentHealth <= 700f && !attackInProgress)
        {
            attackInProgress = true;
            Eclipse();


        }



    }

 

    private IEnumerator JumpOnPlayer()
    {
        attackInProgress = true;



        float targetX = player.transform.position.x;
        float jumpHeight = 10f;

        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        float verticalVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);

        float horizontalVelocity = (targetX - transform.position.x) * 2f;

        rb.linearVelocity = new Vector2(horizontalVelocity, verticalVelocity);


        float timeout = 2f;
        float timeElapsed = 0f;

        while (!AbovePlayer() && timeElapsed < timeout)
        {
            timeElapsed += Time.deltaTime;
            yield return null; 
        }

        rb.linearVelocity = Vector2.down * 8f;

        yield return new WaitUntil(() => isGrounded);

        StartCoroutine(WalkToChamberEdge());
    }

    private IEnumerator WalkToChamberEdge()
    {
        jumpCounter++;

        nextCorner = mapEdges[jumpCounter%2];

        Vector2 direction = (nextCorner.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;

        while (Vector2.Distance(transform.position, nextCorner.position) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextCorner.position, moveSpeed * Time.deltaTime);
            yield return null;
        }


        attackInProgress = false;
    }

    private IEnumerator JumpOverPlayer()
    {
        attackInProgress = true;



        float targetX = player.transform.position.x;
        float jumpHeight = 10f;

        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        float verticalVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);

        float horizontalVelocity = (targetX - transform.position.x);

        rb.linearVelocity = new Vector2(horizontalVelocity, verticalVelocity/1.5f);

        yield return new WaitUntil(() => isGrounded);

        attackInProgress = false;

    }


    void SecondPhase()
    {

    }

    void Eclipse()
    {

    }

    private bool WithinReach()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) <= activationDistance;
    }

    private bool ReachedCorner() => nextCorner.position.x == transform.position.x;

    public bool AbovePlayer()
    {
       
        return (Mathf.Abs(transform.position.x - player.transform.position.x) < 0.5f) && transform.position.y > player.transform.position.y;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
