using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private bool pauseActive = false;
    void Start(){
        gameObject.SetActive(false);
    }

    void Update(){


        if(Input.GetKeyDown(KeyCode.Escape) &&  !pauseActive){
            gameObject.SetActive(true);
            pauseActive = true;
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseActive){
            gameObject.SetActive(false);
            pauseActive = false;
            Time.timeScale = 1f;
        }

        
        
    }
}
