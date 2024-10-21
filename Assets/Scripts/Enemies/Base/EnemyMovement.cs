using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{

    private Transform player;
    private NavMeshAgent agent;
    private Vector3 playerPosition;
    private bool isChasing;


    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPosition = player.position;
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        if (isChasing) LookAtPlayer();

        if (Vector3.Distance(playerPosition, player.position) > 3 && isChasing)
        {
            ChasePlayer();
        }
    }

    // Gets triggered once by the EnemyLogic script on state change, needs to chase player constantly until told otherwise
    public void ChasePlayer()
    {
        isChasing = true;
        playerPosition = player.position;
        agent.SetDestination(playerPosition);
    }

    public void StopChasing()
    {
        agent.SetDestination(agent.transform.position);
        agent.isStopped = false;
    }

    private void LookAtPlayer()
    {
        Vector3 rotation = Quaternion.LookRotation(player.position).eulerAngles;
        rotation.x = 0f;

        transform.rotation = Quaternion.Euler(rotation);
    }
}
