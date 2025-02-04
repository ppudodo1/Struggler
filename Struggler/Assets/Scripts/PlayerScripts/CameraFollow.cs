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
    private bool isFollowingYAxis = false; // New flag for following the player's Y-axis

    private float lockedX; // Stores the X position to lock to

    void Start()
    {
        FindPlayer();

        if (SceneManager.GetActiveScene().name == "Level4")
        {
            Debug.Log(SceneManager.GetActiveScene().name);
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
                isFollowingYAxis = false; // Stop following Y when detaching
            }
            else
            {
                if (isFollowingYAxis)
                {
                    // Keep updating camera Y position while X stays locked
                    transform.position = new Vector3(
                        lockedX, // X stays locked
                        player.position.y + offset.y, // Y follows player
                        transform.position.z
                    );
                }
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

            if (!bossFightSettings)
            {
                isFollowingYAxis = true;
            }
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

       
        if (player.position.x > other.transform.position.x)
        {
            lockedX = other.transform.position.x + Mathf.Abs(player.transform.position.x - other.transform.position.x);
        }
        else
        {
            lockedX = other.transform.position.x - Mathf.Abs(player.transform.position.x - other.transform.position.x);
        }

        
        transform.position = new Vector3(
            lockedX,
            bossFightSettings ? transform.position.y + offset.y : player.position.y + offset.y, // Set initial Y position
            transform.position.z
        );
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
        isFollowingYAxis = false;
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
