using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pattern

    public string levelDiedOn = "SampleScene"; // Session-only data

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load persistent data into the session (e.g., highest unlocked level)
        //currentLevel = PlayerPrefs.GetInt("HighestLevel", 1); 
    }

    public void SaveProgress()
    {
        // Save session data to PlayerPrefs (e.g., when finishing a level)
        //PlayerPrefs.SetInt("HighestLevel", currentLevel);
        //PlayerPrefs.Save();
    }
}
