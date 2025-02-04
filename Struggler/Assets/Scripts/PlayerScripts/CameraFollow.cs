using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float margin = 0.1f;

    private GameObject recentBarrier;
    private Vector2 pointOfDetach = Vector2.zero;
    public static bool touchedBarrier = false;

    private Camera mainCamera;
    private BoxCollider2D boxCollider;

    private bool bossFightSettings = false;
    private bool isStuckToBarrier = false; // Flag to track when camera should stay at barrier

    void Start()
    {
        FindPlayer();

        if (SceneManager.GetActiveScene().name == "Level4")
        {
            bossFightSettings = true;
        }

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

        ResetCamera();
        UpdateColliderSize();
    }

    void Update()
    {
        if (player == null) return;

 
        if (isStuckToBarrier)
        {
            if (IsPlayerBeyondDetachPoint())
            {
                isStuckToBarrier = false;
                touchedBarrier = false;
            }
            else
            {
                return;
            }
        }

        
        LockOnPlayer();
        UpdateColliderSize();
    }

    void LockOnPlayer()
    {
        if (player == null) return;

        if (bossFightSettings)
        {
            transform.position = new Vector3(
                player.position.x + offset.x,
                transform.position.y + offset.y,
                transform.position.z
            );
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
        if (mainCamera == null) return;

        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        boxCollider.size = new Vector2(width - 1.7f, height);
        boxCollider.offset = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barrier"))
        {
            AttachCameraToBarrier(other);
        }
    }

    public void AttachCameraToBarrier(Collider2D other)
    {
        if (player == null) return;


        if (other.transform.position.x > player.transform.position.x)
            pointOfDetach = new Vector2(player.transform.position.x - margin, player.transform.position.y);
        else
            pointOfDetach = new Vector2(player.transform.position.x + margin, player.transform.position.y);

        recentBarrier = other.gameObject;
        touchedBarrier = true;
        isStuckToBarrier = true;


        Vector3 barrierCameraPosition;
        if (player.transform.position.x > other.transform.position.x)
        {
            barrierCameraPosition = new Vector3(
                other.transform.position.x + (player.transform.position.x - other.transform.position.x),
                bossFightSettings ? transform.position.y + offset.y : player.position.y + offset.y,
                transform.position.z
            );
        }
        else
        {
            barrierCameraPosition = new Vector3(
                other.transform.position.x - Math.Abs(player.transform.position.x - other.transform.position.x),
                bossFightSettings ? transform.position.y + offset.y : player.position.y + offset.y,
                transform.position.z
            );
        }

        transform.position = barrierCameraPosition;
    }

    private bool IsPlayerBeyondDetachPoint()
    {
        if (recentBarrier == null) return false;

        if (recentBarrier.transform.position.x > player.transform.position.x)
        {
            return player.transform.position.x < pointOfDetach.x - margin;
        }
        else
        {
            return player.transform.position.x > pointOfDetach.x + margin;
        }
    }

    public void ResetCamera()
    {
 
        FindPlayer();
        touchedBarrier = false;
        isStuckToBarrier = false;
        LockOnPlayer();
    }

    private void FindPlayer()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogError("Player not found in scene!");
        }
    }
}
