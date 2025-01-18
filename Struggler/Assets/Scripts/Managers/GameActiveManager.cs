using UnityEngine;

public class GameActiveManager : MonoBehaviour
{
   
    void Start()
    {
        GameManager.isPaused = false;
        Time.timeScale = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
