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

    public GameObject griffithPic;
    public GameObject gutsPic;


    public GameObject skipCutscene;
    private float skipTimerDefault = 2f;
   private float skipTimer;
    void Start()
    {
        skipTimer = skipTimerDefault;
        skipCutscene.SetActive(false);

        mainCameraAudio = Camera.main.GetComponent<AudioSource>();
        mainCameraAudio.volume = 0.05f;  
        mainCameraAudio.pitch = 1f;

        player.GetComponent<ThrowProjectile>().enabled = false;

        bossBar.SetActive(false);
        griffith.GetComponent<GriffithAI>().enabled = false;

        notManager = notification.GetComponent<NotificationManager>();


    }

    void Update()
    {
        if (skipCutscene.activeSelf && Input.GetKey(KeyCode.E))
        {
            skipTimer -= Time.deltaTime;
            if (skipTimer <= 0f)
            {
                dialogueEnded = true;
                skipCutscene.SetActive(false);
            }
                
        }
        else
            skipTimer = skipTimerDefault;



        if (dialogueEnded)
        {
            skipCutscene.SetActive(false);
            notification.SetActive(false);
            mainCameraAudio.Play();
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

            player.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
            player.GetComponent<Animator>().SetFloat("MoveValue",0f);
            

            player.GetComponent<ThrowProjectile>().enabled = false;

            griffith.GetComponent<GriffithAI>().enabled = false;


            notification.SetActive(true);
            StartCoroutine(Dialogue());
        }
    }

    private IEnumerator Dialogue()
    {
        skipCutscene.SetActive(true);
        notManager.SetNotificationText("I've come to settle this, Griffith!",false);
        yield return new WaitForSeconds(4);

        SwitchImages();
        notManager.SetNotificationText("I was expecting you, Shizo", false);
        yield return new WaitForSeconds(4);

        SwitchImages();
        notManager.SetNotificationText("I won't forgive you what you did to me", false);
        yield return new WaitForSeconds(3);
        notManager.SetNotificationText("To her", false);
        yield return new WaitForSeconds(2);
        notManager.SetNotificationText("To us", false);
        yield return new WaitForSeconds(2);

        SwitchImages();
        notManager.SetNotificationText("I told you Shizo", false);
        yield return new WaitForSeconds(3);
        notManager.SetNotificationText("I will do anything to achieve my dream", false);
        yield return new WaitForSeconds(4);
        notManager.SetNotificationText("Even if it means sacrificing everything", false);
        yield return new WaitForSeconds(4);

        SwitchImages();
        notManager.SetNotificationText("I hope you are ready!", false);
        yield return new WaitForSeconds(4);



        dialogueEnded = true;
    }

    public void SwitchImages()
    {
        if (gutsPic.activeSelf)
        {
            gutsPic.SetActive(false);
            griffithPic.SetActive(true);
        }
        else
        {
            gutsPic.SetActive(true);
            griffithPic.SetActive(false);
        }
    }
}
