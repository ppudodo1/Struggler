using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject leftText;
    public GameObject rightText;
    void Start()
    {
        leftText.SetActive(false);
        rightText.SetActive(false);

    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Barrier")){
            if(collision.transform.position.x > transform.position.x)
                rightText.SetActive(true);
            else
                leftText.SetActive(true);
        }
    }


    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Barrier")){
            if(collision.transform.position.x > transform.position.x)
                rightText.SetActive(false);
            else
                leftText.SetActive(false);
        }
        
    }
}
