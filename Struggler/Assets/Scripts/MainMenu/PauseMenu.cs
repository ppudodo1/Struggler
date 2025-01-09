using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private bool pauseActive = false;
    public GameObject pauseMenu;
    void Start(){
        gameObject.SetActive(false);
    }

    void Update(){
        

        if(Input.GetKeyDown(KeyCode.Escape) &&  !pauseActive){
            pauseMenu.SetActive(true);
            pauseActive = true;
            Time.timeScale = 0f;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseActive){
            pauseMenu.SetActive(false);
            pauseActive = false;
            Time.timeScale = 1f;
        }

        
        
    }
}
