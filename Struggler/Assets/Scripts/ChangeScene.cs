using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float changeTime;
    public string sceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime<=0) {
            SceneManager.LoadScene(sceneName);
        }
    }
}
