using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void LoadLevel(string levelName){
        SceneManager.LoadScene(levelName);
    }
    public void Quit(){
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

 
}
