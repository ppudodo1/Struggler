using UnityEngine;
using TMPro;
public class NotificationManager : MonoBehaviour
{
    private float startYHD = 30f;
    private float startY16 = -75f;
    private float endYHD = 150f;
    private float endY16 = 45f;
    private float endY;
    private float startY;
    public float speed = 2f;

    public GameObject player;

    private bool isActive;
    public bool currentlyUsed;

    //notification.GetComponent<NotificationManager>().GetNotificationText() == ""
    void Start()
    {
        currentlyUsed = false;
        Debug.Log(Screen.currentResolution);
        isActive = false;
        AdaptYBasedOnresolution();
        SetNotificationText("");
        //transform.position = new Vector3(transform.position.x,startY,transform.position.z);
    }

    
    void Update()
    {
        

        AdaptYBasedOnresolution();

        if(isActive){
            MoveNotificationUp();
        }
        else MoveNotificationDown();
       
       
    }

    public void MoveNotificationUp(){

        if(transform.position.y == endY){
           // Debug.Log("Already there");
            return;
        } 

        float step = speed * Time.deltaTime;
        Debug.Log("MoveNotificationUp");
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, endY, transform.position.z),
                step
            );

           

    }

    public void MoveNotificationDown(){

        if(transform.position.y == startY){
           // Debug.Log("Already there");
            return;
        } 

        float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, startY, transform.position.z),
                step
            );

        

    }

    public void SetNotificationText(string message){
        GetComponentInChildren<TMP_Text>().text = message;

    }

    public string GetNotificationText(){
        return GetComponentInChildren<TMP_Text>().text;

    }

    public void SetNotificationActive(bool boolean){
        isActive = boolean;
    }

    public bool GetNotificationActive(){
        return isActive;
    }

    public void AdaptYBasedOnresolution(){
        if(Screen.currentResolution.width == 1600 && Screen.currentResolution.height == 900){
            startY = startY16;
            endY = endY16;
        }
        else if(Screen.currentResolution.width == 1920 && Screen.currentResolution.height == 1080){
            startY = startYHD;
            endY = endYHD;
        }
        else{
            startY = startYHD;
            endY = endYHD;

        } 
    }
}
