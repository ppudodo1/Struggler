using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UnlockLevels : MonoBehaviour
{
    private int unlockedLevels;
    public TextMeshProUGUI  txt;

    void Start(){
        
        CheckUnlockedLevels();
        
    }

    void CheckUnlockedLevels(){
        Image imageToDarken = GetComponent<Image>();
        Button button = GetComponent<Button>();

        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
        //unlockedLevels = GameManager.GetUnlockedLevels();

        int levelNumber = int.Parse(gameObject.name[gameObject.name.Length - 1].ToString());

        if(gameObject.active){
        if(levelNumber > unlockedLevels){
            
            txt.color = new Color(0.851f, 0.400f, 0.400f);
            txt.text += "\nLocked";
            button.interactable = false;
        }
        else if(levelNumber <= unlockedLevels && button.interactable == false){
            button.interactable = true;
            txt.color = Color.white;
            txt.text = txt.text.Remove(7,7);

            
            if(gameObject.name == "Lvl4"){
                txt.text = "Boss Fight";
            }

        }
        }
    }



   
    void Update()
    {
        if (PlayerPrefs.GetInt("MainMenuEasterEgg", 0) == 1){
            CheckUnlockedLevels();
        }
    }
}
