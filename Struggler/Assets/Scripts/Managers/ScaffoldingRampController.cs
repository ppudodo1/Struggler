using UnityEngine;

public class ScaffoldingRampController : MonoBehaviour
{

    public float ttl = 2f;
    private float ttlDefault;
    void Start()
    {
        ttlDefault = ttl;
    }

    void Update()
    {
        ttl -= Time.deltaTime;

        if(ttl < 0f)
        {
            ttl = ttlDefault;
            Destroy(gameObject);
        }
    }
}
