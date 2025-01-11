using UnityEngine;
using TMPro;
public class MonologueManager : MonoBehaviour
{
    
    public GameObject textBoxToDisplay;
    public GameObject notification;
    private bool thrownGrenade = false;


    

    void Start()
    {
       textBoxToDisplay.SetActive(false); 
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !thrownGrenade){
            thrownGrenade = true;
            notification.GetComponent<NotificationManager>().SetNotificationActive(false);

            if(gameObject.name == "Monologue1"){
                Destroy(gameObject);
            }

        }

    }
        //64.5 -75
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

              //  notification.GetComponent<NotificationManager>().SetNotificationText("Press Q to throw a grenade!");
                notification.GetComponent<NotificationManager>().SetNotificationActive(true);
                //notification.GetComponent<TMP_Text>().text = "Press Q to throw a grenade!";
            }
            

            if(gameObject.name != "Monologue1"){
                Destroy(gameObject);
                Destroy(textBoxToDisplay);
            }
        }
    }


}
