using UnityEngine;

public class ClickToJump : MonoBehaviour
{
    private int clickCounter = 0;
    private Animator animator;
    private AudioSource audio;
    public AudioClip theme;

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
        if(clickCounter == 42){
            GameManager.SetUnlockedLevels(4);
            GameManager.SetMainMenuEasterEgg(true);
            audio.pitch = 0.55f;
            audio.PlayOneShot(theme,0.1f);
        }
        Debug.Log(clickCounter);
        animator.SetBool("isJumping",true);

    }
}
