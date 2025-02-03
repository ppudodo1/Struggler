using UnityEngine;

public class ChandelierAI : MonoBehaviour
{


    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }



}
