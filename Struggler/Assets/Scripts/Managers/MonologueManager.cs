using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
public class MonologueManager : MonoBehaviour
{

    private string levelName;
    private TextAsset textAsset;
    private string[] linesOfText;
    private bool wasActivated = false;
    //public bool hasCharacterImage = true;

    public bool isItalic = false;

    private bool hitFirstPlayerCollider = false;

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
            if(!wasActivated && !hitFirstPlayerCollider){

                hitFirstPlayerCollider = true;

                /*
                if (!hasCharacterImage){
        //            notification.GetComponent<NotificationManager>().SetImageActive(false);
                }
                */
                
                notification.SetActive(true);

                notification.GetComponent<NotificationManager>().SetNotificationText(linesOfText[indexOfOperations],isItalic);


               // notification.GetComponent<NotificationManager>().SetActivation(true);
            }

        }


    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            hitFirstPlayerCollider = false;
            if(!wasActivated){
                if (notification == null) return;

                notification.GetComponent<NotificationManager>().ResetNotificationText();
         //   notification.GetComponent<NotificationManager>().SetImageActive(true);
                notification.SetActive(false);
            }

            wasActivated = true;



        }


    }

}
