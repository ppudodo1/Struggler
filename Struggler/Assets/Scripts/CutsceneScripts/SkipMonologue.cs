using UnityEngine;
using System;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndingSceneTimelineBehaviour : MonoBehaviour{
    
private float defaultTimerValue;
public float timer = 2f;  

private bool skipActivated = false;
public float skipToTime = 112.5f;
public PlayableDirector timeline;
public ParticleSystem fairyDust;
public GameObject skipText;

    private bool creditsEnded = false;

    void Start(){
        defaultTimerValue = timer;
    }
    
    void Update(){

        Debug.Log(timeline.time);
        if (Input.GetKey(KeyCode.E)){
            timer -= Time.deltaTime;
        }
        else{
            timer = defaultTimerValue;
        }

        if (timer <= 0 && !skipActivated){
            timeline.time = skipToTime;
            skipActivated = true;

            Destroy(skipText);
        }
        
        if (timeline.time > skipToTime){

            skipActivated = true;
            fairyDust.Stop();
            Destroy(skipText);

        }

        if(timeline.time > 199f)
        {
            creditsEnded = true;
        }
        if(Input.GetKeyDown(KeyCode.Q) && creditsEnded)
        {
            
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.M) && creditsEnded)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

 
    

}
