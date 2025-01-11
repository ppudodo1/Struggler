using UnityEngine;
using TMPro;
public class MonologueManager : MonoBehaviour
{
    
    public GameObject textBoxToDisplay;
    public GameObject notification;

    void Start()
    {
       textBoxToDisplay.SetActive(false); 
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            notification.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("detected");
        if(collision.CompareTag("Player")){
            textBoxToDisplay.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            textBoxToDisplay.SetActive(false);
            if(gameObject.name == "Monologue1"){
                notification.SetActive(true);

                //notification.GetComponent<TMP_Text>().text = "Press Q to throw a grenade!";
            }
            Destroy(gameObject);
        }
    }
}
