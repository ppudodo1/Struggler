using UnityEngine;
using System;
public class GateController : MonoBehaviour
{
    public GameObject player;
    private ParticleSystem gateParticles;
    private float distanceToPlayer;
    public float activationDistance = 1.5f;
    public float particleActivation = 20f;

    public static bool levelCompleted;
    public float speed = 5f;


    private float xDistance;
    private float yDistance;
    private Vector3 gatePosition, playerPosition;
    void Start()
    {
        gateParticles = GetComponent<ParticleSystem>();
        distanceToPlayer = GetDistances();
        levelCompleted = false;
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
    }

    private float GetDistances(){
        gatePosition = transform.position;
        playerPosition = player.transform.position;

        xDistance = gatePosition.x - playerPosition.x;
        yDistance = gatePosition.y - playerPosition.y;

        distanceToPlayer = (float)Math.Sqrt(Math.Pow(yDistance,2) + Math.Pow(xDistance,2));
        
        return distanceToPlayer;
    }




    

    
}
