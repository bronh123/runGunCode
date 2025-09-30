using UnityEngine;
using UnityEngine.AI;

public class Enemy : EnemyStats
{
    [Tooltip("The NavMeshAgent component used for pathfinding")]
    public NavMeshAgent agent;

    [Tooltip("Reference to the player's transform for tracking")]
    public Transform player;

    [Tooltip("Maximum distance at which the enemy can attack the player")]
    public float attackRange = 2f;

    [Tooltip("Minimum distance the enemy tries to maintain from the player")]
    public float bufferRange = 0.5f;

    [Tooltip("Time in seconds between attacks")]
    public float attackCooldown = 2f;

    float nextAttackTime = 0f;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    protected override void OnEnable()
    {
        base.OnEnable();

    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= bufferRange)
        {
            agent.isStopped = true;
            AttemptAttack();
        }
        else if (distanceToPlayer <= attackRange)
        {
            agent.isStopped = false;

            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 targetPosition = player.position - directionToPlayer * bufferRange;


            agent.SetDestination(targetPosition);
            AttemptAttack();
        }
        else
        {
            agent.isStopped = false;

            agent.SetDestination(player.position);
        }
    }

    void AttemptAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            DealDamageToPlayer();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, bufferRange);
    }
}