using UnityEngine;
using System;
public class WizardAI : MonoBehaviour
{
    public GameObject player;

    private GameObject fireball;
    private Rigidbody2D fireballRb;
    private GameObject instantiatedObject;


    public float activationDistance = 8f;
    private SpriteRenderer m_SpriteRenderer;
    private float playerX;
    private Vector3 position;

    private bool isAttacking = false;

    private Animator animator;

    private float timer = 2.08f;

    public float fireballSpeed = 5f;

/*animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && */



    void Start()
    {
        animator = GetComponent<Animator>();
        fireball = Resources.Load<GameObject>("Prefabs/Fireball");
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        position = player.transform.position;
        playerX = position.x;

    }

    void Update()
    {

        if (!isWithinRange())
        {
            timer = 2.08f;
            animator.SetBool("Attack", false);
        }

        else if (isWithinRange())
        {
            animator.SetBool("Attack", true);
            timer -= Time.deltaTime;

            if(timer < 1.04f && !isAttacking)
            {
                isAttacking = true;
                SummonFireball();
            }

        }
        
        if (timer < 0f && isWithinRange()){
            timer = 2.08f;
            isAttacking = false;
        }
        

        SwitchSides();

    }





    public void SwitchSides(){
        position = player.transform.position;
        playerX = position.x;

        if (transform.position.x > playerX && Math.Abs(transform.position.x - playerX) < activationDistance){
            m_SpriteRenderer.flipX = false;
        
        }
        else if (transform.position.x < playerX && Math.Abs(transform.position.x - playerX) < activationDistance){
            m_SpriteRenderer.flipX = true;

        }
    }

    public void SummonFireball(){
        Vector3 fireballVector = Vector3.zero;

    	if (m_SpriteRenderer.flipX)
        {
            fireballVector = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
        }
        else
        {
            fireballVector = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);
        }

        instantiatedObject = Instantiate(fireball, fireballVector, Quaternion.identity);

        Rigidbody2D fireballRb = instantiatedObject.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.transform.position - fireballVector).normalized;
        fireballRb.linearVelocity = direction * fireballSpeed;
        
        /*
        Debug.Log("Nesto se dogodilo");

        if(isWithinRange()){
            animator.SetBool("Attack",true);
        }

        */
    }

    public bool isWithinRange(){
        if(Math.Abs(transform.position.x - playerX) < activationDistance)
            return true;
        
        else
            return false;
    }
}
