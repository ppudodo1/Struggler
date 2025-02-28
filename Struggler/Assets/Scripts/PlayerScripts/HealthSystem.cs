using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;


public class HealthSystem : MonoBehaviour
{

    //razlog float jer ako cemo dodavat difficulty onda na easy mode moze gubiti pola srca
    public int numberOfHearts = 3;
    private int numberOfCollectedHearts = 0;

    public int numberOfShield;
    private string gameOver = "GameOver";
    private Image heartsPrefab;
    public Canvas HUD;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite shield;
    private List<Image> heartsList = new List<Image>();
    private List<GameObject> shieldList = new List<GameObject>();
    public AudioClip collectedHeartSFX;
    private AudioSource audioSource;
    private Vector2 startHeartPosition = new Vector2(-900f,385.5f);
    float positionOfLastHeart = 385.5f;
    void Start()
    {
        numberOfShield = PlayerPrefs.GetInt("NumberOfShield", 0);
        Debug.Log("Number of shield" + numberOfShield);

        audioSource = GetComponent<AudioSource>();


        
        for(int i = 0; i < numberOfHearts; i ++){
            
            GameObject heartObject = new GameObject("HeartImage"); 
            Image summonedImage = heartObject.AddComponent<Image>(); 

            summonedImage.transform.SetParent(HUD.transform);
            summonedImage.sprite = fullHeart;

            RectTransform rectTransform = summonedImage.GetComponent<RectTransform>();


            rectTransform.anchoredPosition = new Vector2(startHeartPosition.x, startHeartPosition.y - i * 70f);
            rectTransform.localScale = new Vector3(0.6f, 0.6f, 1f);

            heartsList.Add(summonedImage);

            if(i == numberOfHearts-1){
                positionOfLastHeart = (startHeartPosition.y - i * 70f) - 70f;
                Debug.Log(positionOfLastHeart);
            }

        }

        for(int i = 0; i < numberOfShield; i ++){
            //brise sliku
            GameObject shieldObject = new GameObject("ShieldImage"); 
            Image summonedImage = shieldObject.AddComponent<Image>(); 

            summonedImage.transform.SetParent(HUD.transform);
            summonedImage.sprite = shield;

            RectTransform rectTransform = summonedImage.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector2(startHeartPosition.x, positionOfLastHeart - i * 70f);
            rectTransform.localScale = new Vector3(0.6f, 0.6f, 1f);

            shieldList.Add(shieldObject);

            if(i == numberOfShield-1){
                positionOfLastHeart = (positionOfLastHeart - i * 70f) - 70f;
                //Debug.Log(positionOfLastHeart);
            }
        }
    }

    void Update(){
        
        //provjera jesu li svi shieldovi unisteni
        if(numberOfShield == 0 && shieldList.Count != 0){
            foreach(GameObject shield in shieldList)
                Destroy(shield);
        }   

    }

    public void removeHeart(){
        
        if(numberOfShield > 0){
            removeShield();
            return;
        }

        numberOfHearts--;
        if(numberOfHearts < 0) numberOfHearts = 0;

        //u oba slucaja sam stavia jednako jer ocu da se na ekranu vidi da si izgubio srce
        if(numberOfHearts >= 0){
            Image heartToRemove = heartsList[numberOfHearts]; 
            heartToRemove.sprite = emptyHeart;
        }
        if(numberOfHearts <= 0){

            // GameManager.Instance.levelDiedOn = SceneManager.GetActiveScene().name;
            //StartCoroutine(transitionToGameOver());
            //Debug.Log(GameManager.Instance.levelDiedOn);
            PlayerPrefs.SetString("LevelDiedOn", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
          //  GameManager.SetLevelDiedOn();

            SceneManager.LoadScene(gameOver);
            

        }

        
    }

    public void removeShield(){
        numberOfShield--;
        
    //    if(numberOfShield < 0) numberOfShield = 0;

        
        if(numberOfShield >= 0){

            GameObject shieldToRemove = shieldList[numberOfShield];
            positionOfLastHeart += 70f;
        /*
            if(shieldToRemove == null){
                Debug.Log("Shield not found");
                return;
            }
        */
            Destroy(shieldToRemove);
        }
    }


//moguc dodatak koda da se dodaju dodatna srca, tipa 4 5 itd, vjv bi trebalo samo  numberOfHearts ++  i ponovno start() pozvat
    public void addHeart(){
        numberOfCollectedHearts++;

        audioSource.PlayOneShot(collectedHeartSFX,0.3f);

        numberOfHearts++;
        if(numberOfHearts > 3) numberOfHearts = 3;

        Image heartToAdd = heartsList[numberOfHearts - 1]; 
        heartToAdd.sprite = fullHeart;

    }

    public void addShield(){
        numberOfShield++;
       // Debug.Log(numberOfShield);
        audioSource.PlayOneShot(collectedHeartSFX,0.3f);
        GameObject shieldObject = new GameObject("ShieldImage"); 
        Image summonedImage = shieldObject.AddComponent<Image>(); 

        summonedImage.transform.SetParent(HUD.transform);
        summonedImage.sprite = shield;

        RectTransform rectTransform = summonedImage.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(startHeartPosition.x, positionOfLastHeart);
        rectTransform.localScale = new Vector3(0.6f, 0.6f, 1f);
        
        positionOfLastHeart -= 70f;
        shieldList.Add(shieldObject);

    }

    IEnumerator transitionToGameOver(){


        yield return StartCoroutine(DarkenAndSlowTheScene());

        
        

    }

    IEnumerator DarkenAndSlowTheScene(){


        /*kod za grayscalanje*/
        //yield return new WaitForSeconds(5f);

        yield return null;

    }
   

    
}
