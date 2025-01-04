using UnityEngine;
using System;
public class ThrowProjectile : MonoBehaviour
{
    private GameObject grenade;
    private GameObject instantiatedObject;
    private Rigidbody2D grenadeRb;
    public Vector3 forceAmount = new Vector3(5f,1f,0f);
    void Start()
    {
        grenade = Resources.Load<GameObject>("Prefabs/grenade");

    }

    void Update(){

        if (Input.GetKeyDown(KeyCode.Q)){
            instantiatedObject = Instantiate(grenade, new Vector2(transform.position.x + 0.5f,transform.position.y), Quaternion.Euler(0, 0, 0));
            grenadeRb = instantiatedObject.GetComponent<Rigidbody2D>();

         
            grenadeRb.AddForce(forceAmount, ForceMode2D.Impulse);
        }


    }
}
