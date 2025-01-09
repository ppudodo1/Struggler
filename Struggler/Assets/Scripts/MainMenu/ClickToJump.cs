using UnityEngine;

public class ClickToJump : MonoBehaviour
{
    private int clickCounter = 0;
    private Animator animator;
    private AudioSource audio;
    public AudioClip theme;
    public int requiredClicks = 42;

    void Start(){
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

    }

    void Update(){
         AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Jumping") && stateInfo.normalizedTime >= 1.0f){
            animator.SetBool("isJumping",false);
            
        }

    }
    void OnMouseDown(){
        clickCounter++;
        if(clickCounter == requiredClicks){
            GameManager.SetUnlockedLevels(4);
            GameManager.SetMainMenuEasterEgg(true);
            audio.pitch = 0.60f;
            audio.PlayOneShot(theme,0.1f);

            GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0.4f, 1f);

        }
        Debug.Log(clickCounter);
       

    }
}
