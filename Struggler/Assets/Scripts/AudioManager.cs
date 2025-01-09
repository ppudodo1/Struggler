using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(){

    GetComponent<AudioSource>().Play();

    }
}
