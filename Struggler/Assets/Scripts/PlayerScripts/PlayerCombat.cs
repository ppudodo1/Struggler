using UnityEngine;

public class PlayerCombat : MonoBehaviour
{


    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float lastAttackTime = 0f;
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime+attackCooldown ) {
            Attack();
            lastAttackTime = Time.time;
        }
    }
    void Attack() {
        animator.SetTrigger("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Enenmy>().TakeDamage(40);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
