using UnityEngine;
using UnityEngine.UI;

public class ClickToJump : MonoBehaviour
{
    private int clickCounter = 0;
    private AudioSource audio;
    public AudioClip theme;
    public int requiredClicks = 42;
    private Button btn;

    void Start(){
        btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        audio = GetComponent<AudioSource>();

    }

    void Update(){
       

    }
    void TaskOnClick(){
        clickCounter++;
        if(clickCounter == requiredClicks){

            PlayerPrefs.SetInt("UnlockedLevels", 4);
            //  GameManager.SetUnlockedLevels(4);

            PlayerPrefs.SetInt("MainMenuEasterEgg", 1);
            //  GameManager.SetMainMenuEasterEgg(true);
            PlayerPrefs.Save();

            audio.pitch = 0.60f;
            audio.PlayOneShot(theme,0.1f);

            GetComponent<Image>().color = new Color(1f, 0.84f, 0.4f, 1f);

        }
        Debug.Log(clickCounter);
       

    }
}
