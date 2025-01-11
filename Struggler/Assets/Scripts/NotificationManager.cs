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

    public static bool isActive = false;

    
    void Start()
    {
        AdaptYBasedOnresolution();
        transform.position = new Vector3(transform.position.x,startY,transform.position.z);
    }

    
    void Update()
    {
        

        AdaptYBasedOnresolution();
        if(isActive){
            MoveNotificationUp();
        }
        else MoveNotificationDown();
       
       if(player.GetComponent<ThrowProjectile>().count == 2 && Input.GetKeyDown(KeyCode.Q)){
            SetNotificationText("You can have 2 grenades active at the time");
            isActive = true;
       }
       else if(player.GetComponent<ThrowProjectile>().count < 2){
            isActive = false;
       }
    }

    public void MoveNotificationUp(){
        
        float step = speed * Time.deltaTime;
        Debug.Log(Screen.currentResolution + "  "+endY);
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, endY, transform.position.z),
                step
            );

           

    }

    public void MoveNotificationDown(){

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

    public void SetNotificationActive(bool boolean){
        isActive = boolean;
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
