using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject leftText;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Barrier")){
            leftText.SetActive(true);
        }
    }


    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Barrier")){
            leftText.SetActive(false);
        }
    }
}
