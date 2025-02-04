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

    private GameObject fireball;
    private Rigidbody2D fireballRb;
    private GameObject instantiatedObject;
    public float fireballSpeed = 5f;

    private bool spawnerTrigger = false;
    public GameObject skeleton;
    private float spawnerTimer;
    public float defaultSpawnerTimer = 2f;


    private GameObject crystal;
    private GameObject instantiatedCrystal;
    private bool crystalActive = false;
    public Transform[] chandelierSpawnAreas;
    private GameObject chandelier;

    private bool eclipseTriggered = false;

    void Start()
    {
        spawnerTimer = defaultSpawnerTimer;
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<Enemy>();
        fireball = Resources.Load<GameObject>("Prefabs/Fireball");
        chandelier = Resources.Load<GameObject>("Prefabs/BossFightChandelier");
        crystal = Resources.Load<GameObject>("Prefabs/Crystal");

        // Debug.Log("Udaljenost "+Mathf.Abs(transform.position.x - player.transform.position.x));
    }

    void Update()
    {

        Debug.Log(enemyScript.currentHealth);

        if (spawnerTrigger)
        {
            spawnerTimer -= Time.deltaTime;
            
        }
        if(spawnerTimer < 0f)
        {
            spawnerTimer = defaultSpawnerTimer;
            SkeletonSpawner();
        }


            
       


        if(enemyScript.currentHealth > 1300f)
        {

            if (!attackInProgress && isGrounded)
            {
                Debug.Log("Attack 1");
                attackInProgress = true;
                StartCoroutine(FirstPhase());
            }


        }
        else if(enemyScript.currentHealth > 700f && !attackInProgress)
        {
            attackInProgress = true;
            StartCoroutine(SecondPhase());
            spawnerTrigger = true;

        }
        else if(enemyScript.currentHealth <= 700f && !attackInProgress && !eclipseTriggered)
        {
            eclipseTriggered = true;
            spawnerTrigger = false;
            attackInProgress = true;
            Eclipse();


        }
        else if(enemyScript.currentHealth <= 700f && eclipseTriggered)
        {
            if (!attackInProgress && isGrounded)
            {
                attackInProgress = true;
                StartCoroutine(FirstPhase());
            }
        }

    }

 

    //prva faza
    private IEnumerator FirstPhase()
    {
        attackInProgress = true;



        float targetX = player.transform.position.x;
        float jumpHeight = 10f;

        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        float verticalVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);

        float horizontalVelocity = (targetX - transform.position.x) * 2f;

        rb.linearVelocity = new Vector2(horizontalVelocity, verticalVelocity);


        float timeout = 1.5f;
        float timeElapsed = 0f;

        while (!AbovePlayer() && timeElapsed < timeout)
        {
            timeElapsed += Time.deltaTime;
            yield return null; 
        }

        rb.linearVelocity = Vector2.down * 6f;

        yield return new WaitUntil(() => isGrounded);

        if(timeElapsed >= timeout)
            StartCoroutine(WalkToCloserChamberEdge());
        else StartCoroutine(WalkToChamberEdge());
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

    //druga faza
    private IEnumerator SecondPhase()
    {
        attackInProgress = true;
        isGrounded = false;

        
        float targetX = player.transform.position.x;
        float jumpHeight = 10f;

        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        float verticalVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);

        float horizontalVelocity = (targetX - transform.position.x);

        rb.linearVelocity = new Vector2(horizontalVelocity, verticalVelocity/1.3f);

        float timeout = 1f;
        float timeElapsed = 0f;


        while (!AbovePlayer() && timeElapsed < timeout)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        FireFireball();
        yield return new WaitForSeconds(0.3f);
        FireFireball();


        yield return new WaitUntil(() => isGrounded);

        StartCoroutine(WalkToCloserChamberEdge());

    }

    private IEnumerator WalkToCloserChamberEdge()
    {
        nextCorner = mapEdges[0];

        foreach(Transform edge in mapEdges)
        {
            if(Mathf.Abs(edge.position.x-transform.position.x) < Mathf.Abs(nextCorner.position.x - transform.position.x))
            {
                nextCorner = edge;
            }
        }

        if (enemyScript.currentHealth <= 700f)
            nextCorner = mapEdges[1];
        


            Vector2 direction = (nextCorner.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;

        while (Vector2.Distance(transform.position, nextCorner.position) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextCorner.position, moveSpeed * Time.deltaTime);
            yield return null;
        }


        attackInProgress = false;
    }

    private void SkeletonSpawner()
    {
        float randomNumber = UnityEngine.Random.Range(-14f, -1f);
        while(Mathf.Abs(randomNumber-player.transform.position.x) < 2f)
            randomNumber = UnityEngine.Random.Range(-14f, -1f);

        Instantiate(skeleton, new Vector3(randomNumber,-3.5f,0f), Quaternion.identity);
    }


    private void Eclipse()
    {
        Crystalize();
        StartCoroutine(ChandelierDrop());
    }

    private void Crystalize()
    {
        if (!crystalActive)
        {
            instantiatedCrystal = Instantiate(crystal, new Vector3(transform.position.x,transform.position.y + 1f,transform.position.z), Quaternion.identity);
            crystalActive = true;
        }

    }

    private IEnumerator ChandelierDrop()
    {
        float summonTimeDefault = 2f;
        float chandelierTimer = 60f;
        float summonTime = summonTimeDefault;

        while (chandelierTimer > 0)
        {
            summonTime -= Time.deltaTime;
            chandelierTimer -= Time.deltaTime;

            if (summonTime <= 0f)
            {
                summonTime = summonTimeDefault;

                int randomNumber = UnityEngine.Random.Range(2, 5);

                int[] randomNumbers = new int[randomNumber];

                for (int i = 0; i < randomNumber; i++)
                {
                    bool unique;
                    do
                    {
                        unique = true;

                        randomNumbers[i] = UnityEngine.Random.Range(0, chandelierSpawnAreas.Length);
                        

                        for (int j = 0; j < i; j++)
                        {
                            if (randomNumbers[i] == randomNumbers[j])
                            {
                                unique = false;
                                break;
                            }
                        }
                    } while (!unique);
                }

                for (int i = 0; i < randomNumber; i++)
                {
                    Instantiate(chandelier, chandelierSpawnAreas[randomNumbers[i]].position, Quaternion.identity);
                }

            }

            yield return null;
        }

        yield return new WaitUntil(() => chandelierTimer < 0f);
        Destroy(instantiatedCrystal);
        attackInProgress = false;
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

    private void FireFireball()
    {
        Vector3 fireballVector = new Vector3(transform.position.x, transform.position.y - 1.7f, transform.position.z);
      

        instantiatedObject = Instantiate(fireball, fireballVector, Quaternion.identity);

        Rigidbody2D fireballRb = instantiatedObject.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.transform.position - fireballVector).normalized;
        fireballRb.linearVelocity = direction * fireballSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
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
