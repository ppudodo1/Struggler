using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private GameObject droppedHeart;

    //samo za griffitha potreban
    public GameObject bossBar;
    private Slider healthBar;
    void Start()
    {

        droppedHeart = Resources.Load<GameObject>("Prefabs/HealingItem");
        currentHealth = maxHealth;

        if(gameObject.name == "Griffith")
        {
            healthBar = bossBar.GetComponentInChildren<Slider>();
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        
        if(gameObject.name == "Griffith")
            healthBar.value = currentHealth;
        
        if (currentHealth <= 0) {
            
            if(gameObject.name == "Griffith")
            {
                bossBar.SetActive(false);
            }


            Destroy(gameObject);

            int randomNumber = UnityEngine.Random.Range(1, 6);
            Vector2 spawnPosition = new Vector2(transform.position.x,transform.position.y);

            if(randomNumber < 3){
                Instantiate(droppedHeart, spawnPosition, Quaternion.Euler(0, 0, 0));
            }

        }
    }
   
    public void StartColorChange(){
        
        //da privremeno iskljuci skriptu tako da je bolji i cisci knockback
        if(gameObject.GetComponent<SkeletonAI>() != null)
            gameObject.GetComponent<SkeletonAI>().enabled = false;
        else if(gameObject.GetComponent<ThrowerAI>() != null)
            gameObject.GetComponent<ThrowerAI>().enabled = false;
        else if(gameObject.GetComponent<SpiritScript>() != null)
            gameObject.GetComponent<SpiritScript>().enabled = false;

        SpriteRenderer enemySpriteRenderer = GetComponent<SpriteRenderer>();
        if (enemySpriteRenderer != null){

            StartCoroutine(ChangeColorCoroutine(enemySpriteRenderer));
        }

        
    }

    private IEnumerator ChangeColorCoroutine(SpriteRenderer enemySpriteRenderer){
        enemySpriteRenderer.color = new Color(1f, 0.8f, 0.8f, 1f);

        yield return new WaitForSeconds(0.5f);

        
        if(gameObject.GetComponent<SkeletonAI>() != null)
            gameObject.GetComponent<SkeletonAI>().enabled = true;
        else if(gameObject.GetComponent<ThrowerAI>() != null)
            gameObject.GetComponent<ThrowerAI>().enabled = true;
        else if(gameObject.GetComponent<SpiritScript>() != null)
            gameObject.GetComponent<SpiritScript>().enabled = true;

        if (enemySpriteRenderer != null){
        enemySpriteRenderer.color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {
            TakeDamage(100);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {
            TakeDamage(100);
        }
    }
}
