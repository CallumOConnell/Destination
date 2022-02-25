using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Destination
{
    public class Enemy : MonoBehaviour
    {
        public float health = 50f;
        public float hearingRadius = 10f;
        public float attackRate = 1f;

        public int damageGiven = 5;

        private Animator animator;

        private Transform target;

        private NavMeshAgent enemyAgent;

        private float nextAttackTime = 0f;

        private bool dead = false;

        private void Start()
        {
            target = PlayerManager.instance.player.transform;

            enemyAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (gameObject.activeSelf && !dead && !target.GetComponent<PlayerVitals>().dead)
            {
                float distance = Vector3.Distance(target.position, transform.position);

                if (distance <= hearingRadius)
                {
                    enemyAgent.SetDestination(target.position);

                    animator.SetBool("isRunning", true);

                    if (distance <= enemyAgent.stoppingDistance)
                    {
                        animator.SetBool("isRunning", false);

                        FaceTarget();

                        Attack();
                    }
                }
                else
                {
                    animator.SetBool("isRunning", false);
                }
            }
        }

        private void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        private void OnDrawGizmosSelected() // Debug for viewing detection radius
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, hearingRadius);
        }

        private IEnumerator SlowDown()
        {
            enemyAgent.speed = 0f;

            yield return new WaitForSeconds(2f);

            enemyAgent.speed = 4f;
        }

        private IEnumerator Die()
        {
            dead = true;

            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<Collider>().enabled = false;

            enemyAgent.isStopped = true;

            animator.SetTrigger("isDead");

            yield return new WaitForSeconds(5f);

            gameObject.SetActive(false);
        }

        private void Attack()
        {
            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("Attack");

                StartCoroutine(SlowDown());

                target.gameObject.GetComponent<PlayerVitals>().ChangeHealth(damageGiven, false);

                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        public void TakeDamage(float amount)
        {
            if (enemyAgent.velocity == Vector3.zero)
            {
                animator.SetTrigger("isHit");
            }
            else
            {
                animator.SetTrigger("isRunHit");
            }

            StartCoroutine(SlowDown());

            health -= amount;

            if (health <= 0) StartCoroutine(Die());
        }
    }
}