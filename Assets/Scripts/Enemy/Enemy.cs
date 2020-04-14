using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public float hearingRadius = 10f;

    private Animator animator;

    private Transform target;

    private NavMeshAgent enemyAgent;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
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
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected() // Debug code?
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hearingRadius);
    }

    public void TakeDamage(float amount)
    {
        if (enemyAgent.isStopped)
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

    private IEnumerator SlowDown()
    {
        enemyAgent.speed = 0f;

        yield return new WaitForSeconds(2.5f);

        enemyAgent.speed = 4f;
    }

    private IEnumerator Die()
    {
        gameObject.GetComponent<AudioSource>().Stop();

        enemyAgent.isStopped = true;

        animator.SetTrigger("isDead");

        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }
}