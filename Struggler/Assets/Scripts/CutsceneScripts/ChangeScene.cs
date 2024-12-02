using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float changeTime;
    public string sceneName;
    private AudioSource audioSource;

    public float decreaseDuration = 3f;  
    public float targetVolume = 0f; 

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime<=0) {
            SceneManager.LoadScene(sceneName);
        }

        if(changeTime <= 5.5f){
            StartCoroutine(FadeOutVolume());

        }
    }

    IEnumerator FadeOutVolume()
    {
        float startVolume = audioSource.volume;  
        float timeElapsed = 0f;
        

        while (timeElapsed < decreaseDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume,targetVolume , timeElapsed / decreaseDuration);
            timeElapsed += Time.deltaTime;  
            yield return null;  
        }

        audioSource.volume = targetVolume;
    }
}
