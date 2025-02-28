using UnityEngine;
using System.Collections;
public class PlayerCombat : MonoBehaviour
{


    private Animator animator;
    public Transform attackPoint;
    public float pushBackForce = -5f;
    public float jumpingPower = 2f;
    public float attackRange = 0.5f;
    private bool attackAnimationFinished = false;
    public LayerMask enemyLayers;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float lastAttackTime = 0f;
 
    void Start(){
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(!PauseMenu.isPaused){
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton5) && Time.time >= lastAttackTime+attackCooldown ) {
                Attack();
                lastAttackTime = Time.time;
            }
            CheckAttackAnimationEnd();
        }
    }
    void Attack() {
        animator.SetTrigger("attack");
        /*
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Enenmy>().TakeDamage(40);
        }
        */
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    void CheckAttackAnimationEnd(){
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.9f){
            if (!attackAnimationFinished){
                attackAnimationFinished = true; 
                Debug.Log("Attack animation finished!");
                OnAttackAnimationEnd();
            }
        }
        else{
            attackAnimationFinished = false;
        }
    }

    void OnAttackAnimationEnd(){

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies) {
            if(enemy == null){
                break;
            } 
            enemy.GetComponent<Enemy>().TakeDamage(50);

            if(enemy.GetComponent<Enemy>().currentHealth > 0){
                enemy.GetComponent<Enemy>().StartColorChange();
                PushBackEnemy(enemy);
            }
        }
    }

    private void PushBackEnemy(Collider2D enemy){
        SpriteRenderer enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();

        Transform enemyTransform = enemy.transform;
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();

        if(rb == null){
            Debug.Log("No RB found!");

            return;
        }
        
        if (enemyTransform.position.x < transform.position.x){

            rb.linearVelocity = new Vector2(pushBackForce, jumpingPower);

        }

        else{

            rb.linearVelocity = new Vector2(-pushBackForce, jumpingPower);

        }


    }
    // Ovdje je isto bug kad ubijes enemya da svejedno trazi unisteni enemy game object
    /*
    private IEnumerator ChangeEnemyColor(SpriteRenderer enemySpriteRenderer){
        Color enemyDefaultColor = enemySpriteRenderer.color;
        enemySpriteRenderer.color = new Color(1f, 0.8f, 0.8f, 1f);

        yield return new WaitForSeconds(0.5f);

        enemySpriteRenderer.color = enemyDefaultColor;
    }
    */
}
