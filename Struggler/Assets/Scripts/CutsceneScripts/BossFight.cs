using System.Collections;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public GameObject notification;
    public GameObject bossBar;

    public GameObject player;
    public GameObject griffith;

    private AudioSource mainCameraAudio;

    private NotificationManager notManager;

    private bool dialogueEnded = false;
    void Start()
    {
        mainCameraAudio = Camera.main.GetComponent<AudioSource>();
        mainCameraAudio.volume = 0.05f;  
        mainCameraAudio.pitch = 0.8f;

        player.GetComponent<ThrowProjectile>().enabled = false;

        bossBar.SetActive(false);
        griffith.GetComponent<GriffithAI>().enabled = false;

        notManager = notification.GetComponent<NotificationManager>();
    }

    void Update()
    {
        if (dialogueEnded)
        {
            Destroy(gameObject);

            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<ThrowProjectile>().enabled = true;

            griffith.GetComponent<GriffithAI>().enabled = true;

            bossBar.SetActive(true);

            mainCameraAudio.volume = 0.2f;
          //  mainCameraAudio.pitch = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            

            player.GetComponent<ThrowProjectile>().enabled = false;

            griffith.GetComponent<GriffithAI>().enabled = false;


            notification.SetActive(true);
            StartCoroutine(Dialogue());
        }
    }

    private IEnumerator Dialogue()
    {
        notManager.SetNotificationText("I've come to settle this, Griffith!",false);
        yield return new WaitForSeconds(4);

        notManager.SetNotificationText("I was expecting you, Shizo", false);
        yield return new WaitForSeconds(4);

        notManager.SetNotificationText("I won't forgive you what you did to me", false);
        yield return new WaitForSeconds(3);
        notManager.SetNotificationText("To her", false);
        yield return new WaitForSeconds(2);
        notManager.SetNotificationText("To us", false);
        yield return new WaitForSeconds(2);

        notManager.SetNotificationText("I told you Shizo", false);
        yield return new WaitForSeconds(3);
        notManager.SetNotificationText("I will do anything to achieve my dream", false);
        yield return new WaitForSeconds(4);
        notManager.SetNotificationText("Even if it means sacrificing everything", false);
        yield return new WaitForSeconds(4);

        notManager.SetNotificationText("I hope you are ready!", false);
        yield return new WaitForSeconds(4);

        dialogueEnded = true;
    }
}
