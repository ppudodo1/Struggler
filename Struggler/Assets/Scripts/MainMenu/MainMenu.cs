using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playMenu;
    public GameObject settingsMenu;
    

    void Start(){


        if(PauseMenu.changingLevels || LevelComplete.changingLevels){
            mainMenu.SetActive(false);
            playMenu.SetActive(true);
            settingsMenu.SetActive(false);

        }
        else{
            mainMenu.SetActive(true);
            playMenu.SetActive(false);
            settingsMenu.SetActive(false);
        }
    }
   
    
    public void LoadLevel(string levelName){
        SceneManager.LoadScene(levelName);
    }
    public void Quit(){
        Application.Quit();
        
      //  UnityEditor.EditorApplication.isPlaying = false;
    }

 
}
