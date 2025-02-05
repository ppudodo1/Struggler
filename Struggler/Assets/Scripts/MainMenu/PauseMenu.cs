using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class PauseMenu : MonoBehaviour
{
    public static bool changingLevels = false;
    public static bool isPaused = false;
    public GameObject pauseMenu;
    private AudioSource audioSource;
    private AudioSource mainCameraAudio;
    public AudioClip clickSound;

    void Start(){
        pauseMenu.SetActive(false);
        changingLevels = false;
        audioSource = GetComponent<AudioSource>();
        mainCameraAudio = Camera.main.GetComponent<AudioSource>();

    }


    void OnDestroy(){
        Time.timeScale = 1f;
    }
    
    void Update(){
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!isPaused)
                PauseGame();
            else if(isPaused)
                ResumeGame();
        }

      
    }
    

    public void PlayClickSound(){
        audioSource.PlayOneShot(clickSound,0.7f);
        

    }

    public void PauseGame(){
        PlayClickSound();

        mainCameraAudio.Pause();
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
       PlayClickSound();

        mainCameraAudio.UnPause();

        pauseMenu.SetActive(false);
        isPaused = false;
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
