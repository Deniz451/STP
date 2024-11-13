using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovement : MonoBehaviour
{

    private Transform player;
    protected NavMeshAgent agent;
    private bool isChasing = false;


    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform ?? player;
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        // tries to find the player if there was no initialy at the start
        if (player == null && GameObject.FindGameObjectWithTag("Player")) player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null && isChasing)
        {
            LookAtPlayer();
            ChasePlayer();
        }
    }

    // gets called from the logic only once, after gets called every frame to update the playes position until said otherwise
    public void ChasePlayer()
    {
        isChasing = true;
        agent.isStopped = false;
        if (player != null) agent.SetDestination(player.position);
    }

    public void StopChasing()
    {
        isChasing = false;
        agent.isStopped = true;    
    }

    private void LookAtPlayer()
    {
        Vector3 rotation = Quaternion.LookRotation(player.position).eulerAngles;
        rotation.x = 0f;

        transform.rotation = Quaternion.Euler(rotation);
    }
}
