using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // The player to follow
    public Vector3 offset;   // Offset from the player's position
    public float margin = 0.1f;

    private GameObject recentBarrier;
    private Vector2 pointOfDetach = Vector2.zero;
    public static bool touchedBarrier = false;

    private Camera mainCamera;
    private BoxCollider2D boxCollider;

    private bool bossFightSettings = false;
    
    

    void Start()
    {

        if (SceneManager.GetActiveScene().name == "Level4")
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            bossFightSettings = true;
        }
            

        LockOnPlayer();

        mainCamera = GetComponent<Camera>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (mainCamera == null)
        {
            Debug.LogError("No Camera component found on this GameObject.");
            return;
        }

        if (boxCollider == null)
        {
            Debug.LogError("No BoxCollider2D component found on this GameObject.");
            return;
        }

        UpdateColliderSize();
    }

    void Update()
    {
        
       // transform.position = new Vector3(transform.position.x,player.transform.position.y + offset.y,transform.position.z);

        if (!touchedBarrier || IsPlayerBeyondDetachPoint())
        {
            LockOnPlayer();
            touchedBarrier = false;
        }

        UpdateColliderSize();
    }

    void LockOnPlayer(){


        if (bossFightSettings)
        {
            transform.position = new Vector3(
            player.position.x + offset.x,
            transform.position.y + offset.y,
            transform.position.z);
        }
        else
        {
            transform.position = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            transform.position.z
        );
        }

        
    }

    void UpdateColliderSize()
    {
       
        float height = 2f * mainCamera.orthographicSize; 
        float width = height * mainCamera.aspect;        

        
        boxCollider.size = new Vector2(width - 1.7f, height);
        boxCollider.offset = Vector2.zero; 
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if (other.CompareTag("Barrier")){
            AttachCameraToBarrier(other);
        }
   
    }

    public void AttachCameraToBarrier(Collider2D other){

        // barijera je na desno
         if(other.transform.position.x > player.transform.position.x)
                pointOfDetach = new Vector2(player.transform.position.x - margin,player.transform.position.y);

        //barijera je na lijevo
        else 
            pointOfDetach = new Vector2(player.transform.position.x + margin,player.transform.position.y);

            recentBarrier = other.gameObject;
          //  Debug.Log("Camera touched barrier and detached\n" + pointOfDetach);


            touchedBarrier = true;

            Vector3 barrierCameraPosition = Vector3.zero;

            if(player.transform.position.x - other.transform.position.x > 0){

                if (!bossFightSettings)
                {

                    barrierCameraPosition = new Vector3(
                    other.transform.position.x + (player.transform.position.x - other.transform.position.x),
                    player.position.y + offset.y,
                    transform.position.z);

                }
                else
                {
                    barrierCameraPosition = new Vector3(
                    other.transform.position.x + (player.transform.position.x - other.transform.position.x),
                    transform.position.y + offset.y,
                    transform.position.z);

                }

            }
            else if(player.transform.position.x - other.transform.position.x < 0){

                if (!bossFightSettings)
                {
                    barrierCameraPosition = new Vector3(
                    other.transform.position.x - Math.Abs(player.transform.position.x - other.transform.position.x),
                    player.position.y + offset.y,
                    transform.position.z);
                }
                else
                {
                    barrierCameraPosition = new Vector3(
                    other.transform.position.x + (player.transform.position.x - other.transform.position.x),
                    transform.position.y + offset.y,
                    transform.position.z);

                }
            }

            transform.position = barrierCameraPosition;

    }

    private bool IsPlayerInMiddle(){
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(player.position);

        float xDifference = Mathf.Abs(viewportPosition.x - 0.5f);
        return xDifference < margin;
    }
    //igrac je desno od zida
    private bool IsPlayerBeyondDetachPoint()
    {
        if (recentBarrier == null) return false;

        if(recentBarrier.transform.position.x > player.transform.position.x){
            return player.transform.position.x < pointOfDetach.x - margin;
        }
        else{
            return player.transform.position.x > pointOfDetach.x + margin;;
        }
            
    }

}
