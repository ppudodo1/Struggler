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
    private int numberOfHearts = 3;
    private int numberOfCollectedHearts = 0;
    private string gameOver = "GameOver";
    private Image heartsPrefab;
    public Canvas HUD;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private List<Image> heartsList = new List<Image>();
    public AudioClip collectedHeartSFX;
    private AudioSource audioSource;
    private Vector2 startHeartPosition = new Vector2(-900f,385.5f);
    public SpriteRenderer backGroundTrees;
    void Start()
    {

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

        }
    }

    void Update(){
        
    }

    public void removeHeart(){
        
        numberOfHearts--;
        if(numberOfHearts < 0) numberOfHearts = 0;

        //u oba slucaja sam stavia jednako jer ocu da se na ekranu vidi da si izgubio srce
        if(numberOfHearts >= 0){
            Image heartToRemove = heartsList[numberOfHearts]; 
            heartToRemove.sprite = emptyHeart;
        }
        if(numberOfHearts <= 0){
            StartCoroutine(transitionToGameOver());

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

    IEnumerator transitionToGameOver(){


        yield return StartCoroutine(DarkenAndSlowTheScene());
        SceneManager.LoadScene(gameOver);

    }

    IEnumerator DarkenAndSlowTheScene(){


        /*kod za grayscalanje*/
        //yield return new WaitForSeconds(5f);

        yield return null;

    }
   

    
}
