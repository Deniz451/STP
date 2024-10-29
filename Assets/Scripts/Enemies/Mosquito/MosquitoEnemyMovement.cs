using UnityEngine;

public class MosquitoEnemyMovement : EnemyMovement
{

    private MosquitoEnemyAttack mosquitoEnemyAttack;


    protected override void Start()
    {
        base.Start();

        mosquitoEnemyAttack = GetComponent<MosquitoEnemyAttack>();
        mosquitoEnemyAttack.OnAttackStart += DisableNavMeshAgent;
        mosquitoEnemyAttack.OnAttackComplete += EnableNavMeshAgent;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void DisableNavMeshAgent()
    {
        agent.enabled = false;
    }

    private void EnableNavMeshAgent()
    {
        agent.enabled = true;
    }
}
