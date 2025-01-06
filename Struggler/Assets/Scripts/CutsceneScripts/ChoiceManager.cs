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
    public TMP_Text choiceResponse;

    private AudioSource audioSource;
    public AudioClip typewriterSFX;

    //sto manja to brze pise
    public float typingSpeed = 0.3f;

    public string deathMessageTop = "You have perished from the reality of normality.";
    public string deathMessageBot = "What do you wish to do?";
    private string choiceResponseGivenUp = "I see,\nanother victim of casuality";
    private string choiceResponseArised = "Very well then,\nStruggler";

    private string ariseScene = "Level2";
   // private string ariseScene;
    public string giveUpScene = "Cutscene";

    private bool hasGivenUp = false;
    private bool choiceMade = false;

    void Start(){


       // ariseScene = GameManager.Instance.levelDiedOn;


        gameOverTop.text = "";
        gameOverBot.text = "";

        audioSource = transform.GetComponent<AudioSource>();
        audioSource.pitch = 0.9f;

        StartCoroutine(SequentialTyping());

        arise.color = Color.yellow;
    }

    
    void Update()
    {
        //if(GameManager.Instance != null){

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

        if(Input.GetKeyDown(KeyCode.Return) && !choiceMade){
            choiceMade = true;

            Destroy(giveUp);
            Destroy(arise);
            Destroy(gameOverTop);
            Destroy(gameOverBot);
            audioSource.Stop();

            
            if(hasGivenUp){
                StartCoroutine(HandleSceneLoadAfterTyping(choiceResponseGivenUp, choiceResponse,giveUpScene));
            }
            else if(!hasGivenUp){
                StartCoroutine(HandleSceneLoadAfterTyping(choiceResponseArised, choiceResponse,ariseScene));
            }

        }
        //}
    }

    IEnumerator TypeText(string message, TMP_Text textWindow){
        audioSource.PlayOneShot(typewriterSFX,0.1f);

        foreach(char c in message.ToCharArray()){

            if(!audioSource.isPlaying)
                audioSource.PlayOneShot(typewriterSFX,0.1f);

            textWindow.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        //da bude gladja tranzicija
        if(choiceMade)
            yield return new WaitForSeconds(2f);
    }

    IEnumerator SequentialTyping(){

    yield return StartCoroutine(TypeText(deathMessageTop, gameOverTop));
    audioSource.Stop();
    yield return StartCoroutine(TypeText(deathMessageBot, gameOverBot));
    audioSource.Stop();


    }

    IEnumerator HandleSceneLoadAfterTyping(string message, TMP_Text textComponent,string sceneName){
    
    //zelim da tekst odabira ispise sporije i bez typewriter zvuka
    audioSource.mute = !audioSource.mute;
    typingSpeed *= 1.5f;

    yield return StartCoroutine(TypeText(message, textComponent));

    //Destroy(GameManager.Instance.gameObject);

    SceneManager.LoadScene(sceneName);
    }
}
