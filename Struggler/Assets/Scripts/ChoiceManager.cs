using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class ChoiceManager : MonoBehaviour
{
    public TextMeshProUGUI giveUp;
    public TextMeshProUGUI arise;
    public TextMeshProUGUI gameOver;

    public string deathMessage = "You have perished from the reality of normality";
    public string ariseScene = "Cutscene";
    private bool hasGivenUp = false;

    void Start()
    {
        arise.color = Color.yellow;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            arise.color = Color.yellow;
            giveUp.color = Color.white;
            hasGivenUp = false;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            arise.color = Color.white;
            giveUp.color = Color.yellow;
            hasGivenUp = true;
        }

        if(Input.GetKeyDown(KeyCode.Return)){

            if(hasGivenUp)
                EditorApplication.isPlaying = false;
            else if(!hasGivenUp){
                SceneManager.LoadScene(ariseScene);
            }

        }
    }
}
