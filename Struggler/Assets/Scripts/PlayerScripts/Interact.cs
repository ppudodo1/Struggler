using UnityEngine;
using System;
public class Interact : MonoBehaviour
{
    
    public GameObject player;
    private Vector3 playerPosition;

    public GameObject E;
    private GameObject initializedE;
    private SpriteRenderer E_spriteRenderer;

    private bool summonedE = false;

    //levitating animation variables
    private Vector3 itemPosition;
    private SpriteRenderer spriteRenderer;
    public float floatHeight = 0.2f;
    public float floatSpeed = 1f;   

    void Start()
    {

        itemPosition = transform.position;
        playerPosition = player.transform.position;
        

    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && summonedE){
            Destroy(gameObject);
            
        }

        playerPosition = player.transform.position;
        playerPosition.y += 1;
        if (initializedE != null){
            initializedE.transform.position = playerPosition;  
        }

        //levitating sword
        float newY = itemPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(itemPosition.x, newY, itemPosition.z);

    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player"))
        { 
            playerPosition.y += 1;
            if(!summonedE){
                initializedE = Instantiate(E,playerPosition,Quaternion.identity);
                summonedE = true;
            }
        }
            

    }

    private void OnTriggerExit2D(Collider2D collision){
       
        if (collision.gameObject.CompareTag("Player")){
            if (initializedE != null){
                Destroy(initializedE);
                summonedE = false;
            }
        }
    }
}
