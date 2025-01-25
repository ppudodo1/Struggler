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
    void Start()
    {
        gateParticles = GetComponent<ParticleSystem>();
        distanceToPlayer = GetDistances();
        levelCompleted = false;
        PauseMenu.isPaused = false;
        
    }

    void Update()
    {
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




    

    
}
