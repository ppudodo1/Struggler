using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class LevelComplete: MonoBehaviour
{
    public static bool changingLevels = false;
    public GameObject finishMenu;
    private AudioSource audioSource;
    public AudioClip clickSound;
    void Start(){
        finishMenu.SetActive(false);
        changingLevels = false;
        audioSource = GetComponent<AudioSource>();

    }


    void OnDestroy(){
        Time.timeScale = 1f;
    }
    
    void Update(){
        
        if(GateController.levelCompleted){
            GameManager.isPaused = true;
            finishMenu.SetActive(true);

            Time.timeScale = 0f;
        }


      
    }
    

    public void PlayClickSound(){
        audioSource.PlayOneShot(clickSound,0.7f);
        

    }

    public void PauseGame(){
        PlayClickSound();

        
        finishMenu.SetActive(true);
        GameManager.isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
       PlayClickSound();

        finishMenu.SetActive(false);
        GameManager.isPaused = false;
        Time.timeScale = 1f;
    }

    public void NextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

   
    public void Quit(){
        PlayClickSound();

        SceneManager.LoadScene("MainMenu");
    }
    public void ChangeLevels(){
        PlayClickSound();


        changingLevels = true;
        SceneManager.LoadScene("MainMenu");
    }
}
