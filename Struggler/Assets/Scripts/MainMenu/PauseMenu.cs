using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class PauseMenu : MonoBehaviour
{
    public static bool changingLevels = false;
    public GameObject pauseMenu;
    private AudioSource audioSource;
    public AudioClip clickSound;
    void Start(){
        pauseMenu.SetActive(false);
        changingLevels = false;
        audioSource = GetComponent<AudioSource>();

    }


    void OnDestroy(){
        Time.timeScale = 1f;
    }
    
    void Update(){
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!GameManager.isPaused)
                PauseGame();
            else if(GameManager.isPaused)
                ResumeGame();
        }

      
    }
    

    public void PlayClickSound(){
        audioSource.PlayOneShot(clickSound,0.7f);
        

    }

    public void PauseGame(){
        PlayClickSound();

        
        pauseMenu.SetActive(true);
        GameManager.isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
       PlayClickSound();

        pauseMenu.SetActive(false);
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
