using UnityEngine;
using TMPro;
public class NotificationManager : MonoBehaviour
{
    public GameObject player;
    private string grenadeTutorial = "<i>press [Q] to throw a grenade";
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(player.GetComponent<ThrowProjectile>().thrownFirstGrenade && GetComponentInChildren<TMP_Text>().text == grenadeTutorial)
            gameObject.SetActive(false);


    }

    public void SetNotificationText(string message){
        GetComponentInChildren<TMP_Text>().text = message;

    }

    
}
