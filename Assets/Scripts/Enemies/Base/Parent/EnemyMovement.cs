using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovement : MonoBehaviour
{
    private EnemyReferences enemyReferences;
    private NavMeshAgent navMeshAgent;
    private bool isChasing = false;

    [SerializeField] private float rayLength;


    protected virtual void Start()
    {
        enemyReferences = GetComponent<EnemyReferences>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        if (enemyReferences.playerTransform != null && isChasing)
        {
            navMeshAgent.destination = enemyReferences.playerTransform.position;
        }
    }

    public void ChasePlayer()
    {
        if (enemyReferences.playerTransform != null)
        {
            isChasing = true;
            navMeshAgent.isStopped = false;
        }
    }

    public void StopChasing()
    {
        isChasing = false;
        navMeshAgent.isStopped = true;
        //enemyReferences.rb.velocity = Vector3.zero;
    }

    private void LookAtPlayer()
    {
        transform.LookAt(enemyReferences.playerTransform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
