using UnityEngine;
using System;
public class SkipCutscene : MonoBehaviour
{
    
private float defaultTimerValue;
public float timer = 2f;  
public ChangeScene changeScene;

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
            changeScene.changeTime = 0f;
        }
        
    }

}
