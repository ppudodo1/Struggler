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

    public float jumpForce = 5f;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<Enemy>();
    }

    void Update()
    {
        if(enemyScript.currentHealth > 1300f && !attackInProgress)
        {
            attackInProgress = true;
            StartCoroutine(FirstPhase());
        }
        else if(enemyScript.currentHealth > 700f && !attackInProgress)
        {
            attackInProgress = true;
            SecondPhase();
        }
        else
        {
            attackInProgress = true;
            Eclipse();
        }



    }

 

    private IEnumerator FirstPhase()
    {
        attackInProgress = true;

        // Jump toward the player
        Vector2 jumpDirection = new Vector2((player.transform.position.x - transform.position.x) * 0.5f, player.transform.position.y + 4f).normalized;
        rb.linearVelocity = jumpDirection * jumpForce;

        // Wait for the boss to start falling
        yield return new WaitUntil(() => rb.linearVelocity.y < 0);

        // Wait for the boss to land (velocity close to zero)
        yield return new WaitUntil(() => Mathf.Abs(rb.linearVelocity.y) < 0.1f && rb.IsTouchingLayers());

        attackInProgress = false; // Now it can attack again
    }

    void SecondPhase()
    {

    }

    void Eclipse()
    {

    }

}
