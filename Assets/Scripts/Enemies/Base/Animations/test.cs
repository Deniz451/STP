using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    private EnemyReferences references;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        references = GetComponent<EnemyReferences>();
    }

    void Update()
    {
        navMeshAgent.SetDestination(references.playerTransform.position);
    }
}
