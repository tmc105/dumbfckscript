using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float attackRange;
    public int attackDamage;
    public float attackCooldown;
    public float aggroRadius;
    private bool aggroed = false;
    private Transform playerTransform;
    private bool canAttack = true;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isAttacking = false;
    public bool isDragon = false;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= aggroRadius)
            {
                aggroed = true;
            }

            if (aggroed)
            {
                if (distanceToPlayer <= attackRange && !isAttacking)
                {
                    FaceTarget();
                    agent.isStopped = true;
                    if (isDragon)
                    {
                        int randomNumber = Random.Range(1, 3);
                        animator.SetTrigger("claw" + randomNumber.ToString());
                        Attack();
                    }

                    else
                    {
                        animator.SetTrigger("performAttack");
                        Attack();
                    }
                }
                else if (distanceToPlayer > attackRange)
                {
                    agent.isStopped = false;
                    MoveTowardsPlayer();
                }

            }
        }
        bool isRunning = agent.remainingDistance > agent.stoppingDistance;
        animator.SetBool("isRunning", isRunning);
    }

    void FaceTarget()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
    }

    void MoveTowardsPlayer()
    {
        agent.SetDestination(playerTransform.position);
    }

    void Attack()
    {
        if (canAttack)
        {
            isAttacking = true;
            PlayerStats playerStats = playerTransform.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(attackDamage, "Physical");
            }

            canAttack = false;
            Invoke("ResetAttackCooldown", attackCooldown);
        }
    }

    void ResetAttackCooldown()
    {
        canAttack = true;
        isAttacking = false;
    }
}

