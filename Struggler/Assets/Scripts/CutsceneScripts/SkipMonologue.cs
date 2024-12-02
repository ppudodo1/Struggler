using UnityEngine;
using System;
using UnityEngine.Playables;

public class EndingSceneTimelineBehaviour : MonoBehaviour{
    
private float defaultTimerValue;
public float timer = 2f;  

private bool skipActivated = false;
public float skipToTime = 112.5f;
public PlayableDirector timeline;
public ParticleSystem fairyDust;
public GameObject skipText;

    void Start(){
        defaultTimerValue = timer;
    }
    
    void Update(){
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
    }

 
    

}
