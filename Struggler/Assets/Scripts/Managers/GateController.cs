using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GateController : MonoBehaviour
{
    public GameObject player;
    public GameObject healthSystem;
    private ParticleSystem gateParticles;
    private float distanceToPlayer;
    public float activationDistance = 1.5f;
    public float particleActivation = 20f;

    public static bool levelCompleted;
    public float speed = 5f;


    private float xDistance;
    private float yDistance;
    private bool increasedLevel = false;
    private Vector3 gatePosition, playerPosition;

    //samo za bossfight
    private AudioSource mainCameraAudio;

    void Start()
    {
        //samo za bossfight
        mainCameraAudio = Camera.main.GetComponent<AudioSource>();


        gateParticles = GetComponent<ParticleSystem>();
        distanceToPlayer = GetDistances();
        levelCompleted = false;
        PauseMenu.isPaused = false;
        
    }

    void Update()
    {
        //samo za bossfight
        if (SceneManager.GetActiveScene().name == "Level4")
        {
            if (CheckIfBossDefeated())
            {
                transform.position = new Vector3(-8.02284813f, -2.50999999f, 0);
                mainCameraAudio.Stop();
            }
        }

        distanceToPlayer = GetDistances();

        if(distanceToPlayer > particleActivation){
            gateParticles.Stop();

        }
        else gateParticles.Play(true);
        /*
        if(distanceToPlayer < activationDistance){
            Debug.Log("Close to it, pulling the player");


        }
        */
        if(levelCompleted && !increasedLevel){
            increasedLevel = true;
            
           UpdateGameManagerValues();

        }
    }

    private float GetDistances(){
        gatePosition = transform.position;
        playerPosition = player.transform.position;

        xDistance = gatePosition.x - playerPosition.x;
        yDistance = gatePosition.y - playerPosition.y;

        distanceToPlayer = (float)Math.Sqrt(Math.Pow(yDistance,2) + Math.Pow(xDistance,2));
        
        return distanceToPlayer;
    }

    public void UpdateGameManagerValues(){

        //int unlockedLevels = GameManager.GetUnlockedLevels();
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);

        if (SceneManager.GetActiveScene().name.EndsWith(unlockedLevels.ToString()))
            unlockedLevels++;

        if(unlockedLevels > 4) unlockedLevels = 4;
        else if(unlockedLevels < 1) unlockedLevels = 1;

        PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
        //GameManager.SetUnlockedLevels(unlockedLevels);

        PlayerPrefs.SetInt("NumberOfShield", healthSystem.GetComponent<HealthSystem>().numberOfShield);
        //GameManager.SetNumberOfShield(healthSystem.GetComponent<HealthSystem>().numberOfShield);
            
    }

    public bool CheckIfBossDefeated()
    {
        GameObject griffithObject = GameObject.Find("Griffith");

        if (griffithObject != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }




    

    
}
