using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startpos;
    public GameObject cam;
    public float parallaxEffect;
    public float buffer = 1f; // Small buffer to preload next segment

    void Start()
    {
        startpos = transform.position.x;
      
    }

    void Update()
    {
       
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

    }
}
