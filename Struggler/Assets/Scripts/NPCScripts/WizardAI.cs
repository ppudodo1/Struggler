using UnityEngine;
using System;
public class WizardAI : MonoBehaviour
{
    public GameObject player;

    private GameObject fireball;
    public float activationDistance = 8f;
    private SpriteRenderer m_SpriteRenderer;
    private float playerX;
    private Vector3 position;




    void Start()
    {
        fireball = Resources.Load<GameObject>("Prefabs/Fireball");
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        position = player.transform.position;
        playerX = position.x;

    }

    void Update()
    {
        SwitchSides();
    }

    public void SwitchSides(){
        position = player.transform.position;
        playerX = position.x;

        if (transform.position.x > playerX && Math.Abs(transform.position.x - playerX) < activationDistance){
            m_SpriteRenderer.flipX = false;
        
        }
        else if (transform.position.x < playerX && Math.Abs(transform.position.x - playerX) < activationDistance){
            m_SpriteRenderer.flipX = true;

        }
    }
}
