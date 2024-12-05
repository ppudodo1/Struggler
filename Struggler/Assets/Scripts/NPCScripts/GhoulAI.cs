using UnityEngine;
using System;
public class GhoulAI : MonoBehaviour
{

    //ghoul
    private Vector2 ghoulPosition;
    private float degrees;
    public float maxDetectionDistance = 10f;
    public float speed = 3f;
    private float xDistance;
    private float yDistance;
    private float distanceToPlayer;

    //player
    public GameObject player;
    private Vector2 playerPosition;
    private SpriteRenderer playerSprite;

    //rest
    private float timerToAttack;
    public float timerToAttackDefault = 2f;

    void Start()
    {
        GetDistances();
        timerToAttack = timerToAttackDefault;
        playerSprite = player.GetComponent<SpriteRenderer>();

    }

    void Update(){

        GetDistances();

        if(Math.Abs(xDistance) < maxDetectionDistance){
            float angleRadians = Mathf.Atan2(yDistance, xDistance);
            degrees = angleRadians * Mathf.Rad2Deg;

            //dokle god originalni sprite gleda u lijevo, ne treba dodavati nista, ako tipa gleda desno, treba napisati degress - 180f da bi tocno gledao
            transform.eulerAngles = new Vector3(0f, 0f, degrees);
        }

        //znaci da player gleda u livo a ghoul je desno      
        if(player.transform.localScale.x < 0 && xDistance > 0 && Math.Abs(xDistance) < maxDetectionDistance){
            timerToAttack -= Time.deltaTime;
            Debug.Log(timerToAttack);
        }
        //obrnuto
        else if(player.transform.localScale.x > 0 && xDistance < 0 && Math.Abs(xDistance) < maxDetectionDistance){
            timerToAttack -= Time.deltaTime;
            Debug.Log(timerToAttack);
        }
        else{
            timerToAttack = timerToAttackDefault;
            Warp();
        }

        if(timerToAttack < 0){
           Attack();
        }



    }

    private void GetDistances(){
        ghoulPosition = transform.position;
        playerPosition = player.transform.position;

        xDistance = ghoulPosition.x - playerPosition.x;
        yDistance = ghoulPosition.y - playerPosition.y;
        distanceToPlayer = (float)Math.Sqrt(Math.Pow(yDistance,2) + Math.Pow(xDistance,2));
    }

    private void Attack(){

        /*
        if(xDistance < playerPosition.x){
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if(xDistance > playerPosition.x){
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        */

        transform.Translate(Vector3.left * speed * Time.deltaTime);

    }

    private void Warp(){
        if(distanceToPlayer < 2.5f){
            Debug.Log("Blizu je!");
            transform.Translate(new Vector3(2f, 2f, 0));
        }
        if(yDistance < 0){
            transform.position = new Vector2(transform.position.x, UnityEngine.Random.Range(playerPosition.y,2.5f));
        }
    }
}
