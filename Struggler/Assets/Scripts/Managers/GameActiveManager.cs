using UnityEngine;

public class GameActiveManager : MonoBehaviour
{
   
    void Start()
    {
        PauseMenu.isPaused = false;
        Time.timeScale = 1f;

        //OVO POKRENUT PRIJE BUILDANJA IGRICE

        /*
        #if UNITY_EDITOR
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs cleared in Editor mode");
        #endif
        */
    }

    void Update()
    {
        
    }
}
