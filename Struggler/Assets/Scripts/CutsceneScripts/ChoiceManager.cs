using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChoiceManager : MonoBehaviour
{
    public TMP_Text giveUp;
    public TMP_Text arise;
    public TMP_Text gameOverTop;
    public TMP_Text gameOverBot;

    private AudioSource audioSource;
    public AudioClip typewriterSFX;
    public float typingSpeed = 0.3f;

    public string deathMessageTop = "You have perished from the reality of normality.";
    public string deathMessageBot = "What do you wish to do?";

    public string ariseScene = "Cutscene";
    private bool hasGivenUp = false;

    void Start(){

        gameOverTop.text = "";
        gameOverBot.text = "";

        audioSource = transform.GetComponent<AudioSource>();
        audioSource.pitch = 0.9f;

        StartCoroutine(SequentialTyping());

        arise.color = Color.yellow;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            arise.color = Color.yellow;
            giveUp.color = Color.white;
            hasGivenUp = false;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            arise.color = Color.white;
            giveUp.color = Color.yellow;
            hasGivenUp = true;
        }

        if(Input.GetKeyDown(KeyCode.Return)){

            if(hasGivenUp)
                EditorApplication.isPlaying = false;
            else if(!hasGivenUp){
                SceneManager.LoadScene(ariseScene);
            }

        }
    }

    IEnumerator TypeText(string message, TMP_Text textWindow){
        audioSource.PlayOneShot(typewriterSFX,0.2f);

        foreach(char c in message.ToCharArray()){

            if(!audioSource.isPlaying)
                audioSource.PlayOneShot(typewriterSFX,0.2f);

            textWindow.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    IEnumerator SequentialTyping(){

    yield return StartCoroutine(TypeText(deathMessageTop, gameOverTop));
    audioSource.Stop();
    yield return StartCoroutine(TypeText(deathMessageBot, gameOverBot));
    audioSource.Stop();


    }
}
