using UnityEngine;
using System;
public class WizardAI : MonoBehaviour
{
    public GameObject player;

    private GameObject fireball;
    private GameObject instantiatedObject;


    public float activationDistance = 8f;
    private SpriteRenderer m_SpriteRenderer;
    private float playerX;
    private Vector3 position;
    private bool isPlayingWizardAttack = false;
    private Animator animator;





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
        /*
         if (animator.GetCurrentAnimatorStateInfo(0).IsName("WizardAttack"))
        {
            }
        else if (isPlayingWizardAttack)
        {
            Debug.Log("WizardAttack animation was interrupted or ended.");
            isPlayingWizardAttack = false;
        }

            isPlayingWizardAttack = true;
        */
            if (/*animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && */Math.Abs(transform.position.x - playerX) < activationDistance)
            {
                isPlayingWizardAttack = false;
                SummonFireball();

                animator.SetBool("Attack", true);

            }
            else
                animator.SetBool("Attack", false);

        

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
        if(m_SpriteRenderer.flipX){

            instantiatedObject = Instantiate(fireball, new Vector2(transform.position.x + 1.5f,transform.position.y), Quaternion.Euler(0, 0, 0));

            //grenadeRb = instantiatedObject.GetComponent<Rigidbody2D>();
            //grenadeRb.AddForce(new Vector2(UnityEngine.Random.Range(20, 30), UnityEngine.Random.Range(10, 20)), ForceMode2D.Impulse);
        }

        else if(!m_SpriteRenderer.flipX){

            instantiatedObject = Instantiate(fireball, new Vector2(transform.position.x - 1.5f,transform.position.y), Quaternion.Euler(0, 0, 0));

            //grenadeRb = instantiatedObject.GetComponent<Rigidbody2D>();
            //grenadeRb.AddForce(new Vector2(UnityEngine.Random.Range(-30,-20),UnityEngine.Random.Range(10,20)), ForceMode2D.Impulse);
        }
    }
}
