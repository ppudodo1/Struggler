using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NotificationManager : MonoBehaviour
{
    public GameObject player;
    private string grenadeTutorial = "Press [Q] to throw a grenade";
    public float typingSpeed = 0.3f;
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (player.GetComponent<ThrowProjectile>() != null)
        {
            if (player.GetComponent<ThrowProjectile>().thrownFirstGrenade && GetComponentInChildren<TMP_Text>().text == grenadeTutorial)
            {

                gameObject.SetActive(false);

            }
        }


    }

    public void SetNotificationText(string message,bool isItalic)
    {

        if(isItalic)
            GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Italic;

        StartCoroutine(TypeText(message, GetComponentInChildren<TMP_Text>()));
       // GetComponentInChildren<TMP_Text>().text = message;

    }

    public void SetImageActive(bool boolean){
        Image[] childImages =  GetComponentsInChildren<Image>();
        childImages[1].enabled = false;
    }

    public void ResetNotificationText()
    {
        

        GetComponentInChildren<TMP_Text>().text = "";
        GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Bold;
    }

    void OnEnable(){

    }

    void OnDisable(){
        
    }

    IEnumerator TypeText(string message, TMP_Text textWindow)
    {
        //audioSource.PlayOneShot(typewriterSFX, 0.1f);
        textWindow.text = "";

        foreach (char c in message.ToCharArray())
        {
            /*
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(typewriterSFX, 0.1f);
            */

            textWindow.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        //da bude gladja tranzicija
        /*
        if (choiceMade)
            yield return new WaitForSeconds(2f);
        */
    }


}
