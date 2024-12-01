using UnityEngine;
using System;
using UnityEngine.Playables;

public class EndingSceneTimelineBehaviour : MonoBehaviour{
    
private float defaultTimerValue;
public float timer = 2f;  

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

        if (timer <= 0){
            timeline.time = 112.5f;
            Destroy(skipText);
        }
        
        if (timeline.time > 112.5f){
            fairyDust.Stop();
            Destroy(skipText);

        }
    }

    

}
