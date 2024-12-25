using UnityEngine;

public class Enenmy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject droppedHeart;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            
        Destroy(gameObject);

        int randomNumber = UnityEngine.Random.Range(1, 6);
        Vector2 spawnPosition = new Vector2(transform.position.x,transform.position.y);

        if(randomNumber < 3){
            Instantiate(droppedHeart, spawnPosition, Quaternion.Euler(0, 0, 0));
        }

        }
    }
   
}
