using UnityEngine;

namespace Destination
{
    public class MeleeBase : MonoBehaviour
    {
        [Space, Header("Audio Settings")]
        public AudioSource audioSource;

        public AudioClip[] attackCries;

        [Space, Header("Attack Settings")]
        public float attackRange = 0.5f;
        public float attachRate = 2f;

        public int attackDamage = 20;

        public Transform attackPoint;

        public LayerMask enemyLayer;

        public SwitchWeapon switchWeapon;

        private float nextAttackTime = 0f;

        private Animator animator;

        private void Awake() => animator = GetComponent<Animator>();

        private void Update()
        {
            if (switchWeapon.currentWeapon == "Crowbar")
            {
                if (Time.time >= nextAttackTime)
                {
                    animator.SetBool("isAttack", false);

                    if (Input.GetButtonDown("Attack"))
                    {
                        Attack();

                        nextAttackTime = Time.time + 1f / attachRate;
                    }
                }
            }
        }

        private void Attack()
        {
            animator.SetBool("isAttack", true);

            audioSource.PlayOneShot(attackCries[Random.Range(0, attackCries.Length)]);

            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

            if (hitEnemies.Length > 0)
            {
                foreach (Collider enemy in hitEnemies)
                {
                    if (enemy != null)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                    }
                }
            }
        }

        private void OnDrawGizmosSelected() // Debug for testing attack range
        {
            if (attackPoint == null) return;

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}