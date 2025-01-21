using UnityEngine;
using System;
public class ThrowProjectile : MonoBehaviour
{
    private GameObject grenade;
    private GameObject instantiatedObject;
    private Rigidbody2D grenadeRb;
    private SpriteRenderer sr;
    public int count;
    
    public int maxGrenades = 2;
    public Vector3 rightThrowVector = new Vector3(5f,1f,0f);
    public bool thrownFirstGrenade = false;
    private bool moreThan2Grenades = false;


    //za funkcije notifikacije
    public GameObject notification;

    void Start(){
        count  = GameObject.FindGameObjectsWithTag("Projectile").Length;
        grenade = Resources.Load<GameObject>("Prefabs/grenade");
        sr = GetComponent<SpriteRenderer>();

    }

    void Update(){
        
        count = GameObject.FindGameObjectsWithTag("Projectile").Length;

        if(!GameManager.isPaused){

                /*
            //nazalost njega ne mogu drugacije blokirati, trebao bi i njemu dolje staviti da bool postaje true al onda ovaj if se nikad nebi izvrsio, probat cemo sutra
            if(count < maxGrenades && !notification.GetComponent<NotificationManager>().currentlyUsed){
                notification.GetComponent<NotificationManager>().SetNotificationActive(false);
            }
            */

            if (Input.GetKeyDown(KeyCode.Q) && count < maxGrenades){
                notification.SetActive(false);

                thrownFirstGrenade = true;
                Vector3 localScale = transform.localScale;

                //ovisno di sprite gleda
                if(localScale.x < 0){
                    Vector3 leftThrowVector = new Vector3(-rightThrowVector.x,rightThrowVector.y,rightThrowVector.z);

                    instantiatedObject = Instantiate(grenade, new Vector2(transform.position.x - 1.6f,transform.position.y), Quaternion.Euler(0, 0, 0));
                    grenadeRb = instantiatedObject.GetComponent<Rigidbody2D>();
                    grenadeRb.AddForce(leftThrowVector, ForceMode2D.Impulse);
                }
                else if(localScale.x > 0){
                    instantiatedObject = Instantiate(grenade, new Vector2(transform.position.x + 1.6f,transform.position.y), Quaternion.Euler(0, 0, 0));
                    grenadeRb = instantiatedObject.GetComponent<Rigidbody2D>();
                    grenadeRb.AddForce(rightThrowVector, ForceMode2D.Impulse);
                }
                grenadeRb.AddTorque(-2, ForceMode2D.Impulse);

            
            }
            if(count < maxGrenades && moreThan2Grenades){
                moreThan2Grenades = false;
                notification.SetActive(false);
           //     notification.GetComponent<NotificationManager>().SetImageActive(true);



            }
            else if(count == maxGrenades && Input.GetKeyDown(KeyCode.Q)){
                    moreThan2Grenades = true;

                //    notification.GetComponent<NotificationManager>().SetImageActive(false);
                    notification.GetComponent<NotificationManager>().SetNotificationText("<i>You can have 1 grenade active at the time");
                    notification.SetActive(true);
            }
            
            
        }
    }
}
