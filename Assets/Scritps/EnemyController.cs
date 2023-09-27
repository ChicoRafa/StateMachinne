using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState { IDLE, CHASE, ATTACK };
    public EnemyState state;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public Transform player;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.IDLE;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.IDLE:
                agent.isStopped = true;

                if (Vector3.Distance(player.position, transform.position) <= detectionRange)
                {
                    state = EnemyState.CHASE;
                    Debug.Log("changed to chase");
                }
                break;
            case EnemyState.CHASE:
                agent.isStopped = false;
                agent.SetDestination(player.position);

                if (Vector3.Distance(player.position, transform.position) > detectionRange)
                {
                    state = EnemyState.IDLE;
                    Debug.Log("changed to idle");
                }
                else if (Vector3.Distance(player.position, transform.position) <= attackRange)
                {
                    state = EnemyState.ATTACK;
                    Debug.Log("changed to attack");
                }
                break;
            case EnemyState.ATTACK:
                agent.isStopped = true;
                if (Vector3.Distance(player.position, transform.position) > attackRange)
                {
                    state = EnemyState.CHASE;
                    Debug.Log("changed to chase");
                }
                break;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
