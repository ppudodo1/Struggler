using UnityEngine;
using System.Collections.Generic;
using System;
public class PlatformSummonerController : MonoBehaviour
{
    private GameObject summonedPlatform;
    private List<GameObject> platformList = new List<GameObject>();

    public float platformSpeed = 5f;
    public bool goingUp;

    //public float ttl = 2f;
    //private float ttlDefault;

    public float frequency = 2f;
    private float frequencyDefault;

    private bool firstPlatformSummoned = false;

    void Start()
    {
       
        frequencyDefault = frequency;
        summonedPlatform = Resources.Load<GameObject>("Prefabs/SummonedRamp");

        if (summonedPlatform == null) Debug.LogError("AAA");
    }

    void Update()
    {
        frequency -= Time.deltaTime;
     
        if(frequency < 0f)
        {
            frequency = frequencyDefault;
            SummonPlatform();
        }

        /*
        if (firstPlatformSummoned)
        {
            ttl -= Time.deltaTime;
            
        }
        if (ttl < 0f)
        {
            ttl = Math.Abs(ttlDefault - frequencyDefault);
            DeleteFirstRamp();
        }
        */
    }

    public void SummonPlatform()
    {
        firstPlatformSummoned = true;
        Vector2 directionVector = Vector2.zero;
        Vector2 summonPosition = Vector2.zero;
        GameObject instantiatedObject = null;

        if (goingUp)
        {
            directionVector = Vector2.up;
            summonPosition = new Vector2(transform.position.x, transform.position.y - 20f);
        }

        else
        {
            directionVector = Vector2.down;
            summonPosition = new Vector2(transform.position.x, transform.position.y + 20f);

        }

        instantiatedObject = Instantiate(summonedPlatform, summonPosition, Quaternion.identity);

        Rigidbody2D platformRb = instantiatedObject.GetComponent<Rigidbody2D>();
        platformRb.linearVelocity = directionVector * platformSpeed;


        //platformList.Add(instantiatedObject);
    }

    public void DeleteFirstRamp()
    {
        Destroy(platformList[0]);
        platformList.RemoveAt(0);
    }
}
