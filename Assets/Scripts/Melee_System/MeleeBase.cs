using UnityEngine;

namespace Destination
{
    public class MeleeBase : MonoBehaviour
    {
        [Space, Header("Animator Settings")]
        public Animator animator;

        [Space, Header("Audio Settings")]
        public AudioSource audioSource;

        public AudioClip[] attackCries;

        [Space, Header("Attack Settings")]
        public float attackRange = 0.5f;
        public float attachRate = 2f;

        public int attackDamage = 20;

        public Transform attackPoint;

        public LayerMask enemyLayer;

        private float nextAttackTime = 0f;

        private void Update()
        {
            // Check player has crowbar as current weapon

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetButtonDown("Attack"))
                {
                    Attack();

                    nextAttackTime = Time.time + 1F / attachRate;
                }
            }
        }

        private void Attack()
        {
            animator.SetTrigger("isAttack");

            //audioSource.PlayOneShot(attackCries[Random.Range(0, attackCries.Length)]);

            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null) return;

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}