using UnityEngine;
using UnityEngine.InputSystem;

namespace Destination
{
    public class MeleeBase : MonoBehaviour
    {
        [Space, Header("Attack Settings")]
        public float attackRange = 0.5f;
        public float attackRate = 2f;

        public int attackDamage = 20;

        public Transform attackPoint;

        public LayerMask enemyLayer;

        public SwitchWeapon switchWeapon;

        private float nextAttackTime = 0f;

        private Animator animator;

        private void Awake() => animator = GetComponent<Animator>();

        private void Update()
        {
            Gamepad gamepad = Gamepad.current;

            if (gamepad == null) return;

            if (gamepad.rightShoulder.wasPressedThisFrame)
            {
                if (switchWeapon.currentWeapon == "Melee")
                {
                    if (Time.time >= nextAttackTime)
                    {
                        animator.SetBool("isAttack", false);

                        Attack();

                        nextAttackTime = Time.time + 1f / attackRate;
                    }
                }
            }
        }

        private void Attack()
        {
            animator.SetBool("isAttack", true);

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

        private void OnDrawGizmosSelected() // Debug for displaying the attack range
        {
            if (attackPoint == null) return;

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}