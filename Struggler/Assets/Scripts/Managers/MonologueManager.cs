using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MonologueManager : MonoBehaviour
{

    private string levelName;
    private TextAsset textAsset;
    private string[] linesOfText;
    private bool wasActivated = false;


    public int indexOfOperations = 0;
    public GameObject notification;

    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
        Debug.Log(levelName);
        textAsset = Resources.Load<TextAsset>("TextFiles/"+levelName+"Text");

        if (textAsset != null)
        {
            linesOfText = textAsset.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else
        {
            Debug.LogError("TextAsset not found! Make sure the file is in the Resources folder.");
        }
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            
            if(!wasActivated){
                wasActivated = true;
                notification.SetActive(true);
                notification.GetComponent<NotificationManager>().SetNotificationText(linesOfText[indexOfOperations]);
               // notification.GetComponent<NotificationManager>().SetActivation(true);
            }

        }


    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            notification.SetActive(false);
            notification.GetComponent<NotificationManager>().SetNotificationText("");

        }


    }

}
