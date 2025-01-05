using UnityEngine;
using System;
public class ThrowProjectile : MonoBehaviour
{
    private GameObject grenade;
    private GameObject instantiatedObject;
    private Rigidbody2D grenadeRb;
    private SpriteRenderer sr;
    
    public int maxGrenades = 2;
    public Vector3 rightThrowVector = new Vector3(5f,1f,0f);

    void Start(){

        grenade = Resources.Load<GameObject>("Prefabs/grenade");
        sr = GetComponent<SpriteRenderer>();

    }

    void Update(){
        int count = GameObject.FindGameObjectsWithTag("Projectile").Length;

        if (Input.GetKeyDown(KeyCode.Q) && count < maxGrenades){

            Vector3 localScale = transform.localScale;

            //ovisno di sprite gleda
            if(localScale.x < 0){
                Vector3 leftThrowVector = new Vector3(-rightThrowVector.x,rightThrowVector.y,rightThrowVector.z);

                instantiatedObject = Instantiate(grenade, new Vector2(transform.position.x - 1.4f,transform.position.y), Quaternion.Euler(0, 0, 0));
                grenadeRb = instantiatedObject.GetComponent<Rigidbody2D>();
                grenadeRb.AddForce(leftThrowVector, ForceMode2D.Impulse);
            }
            else if(localScale.x > 0){
                instantiatedObject = Instantiate(grenade, new Vector2(transform.position.x + 1.4f,transform.position.y), Quaternion.Euler(0, 0, 0));
                grenadeRb = instantiatedObject.GetComponent<Rigidbody2D>();
                grenadeRb.AddForce(rightThrowVector, ForceMode2D.Impulse);
            }
            grenadeRb.AddTorque(-2, ForceMode2D.Impulse);

            
        }


    }
}
