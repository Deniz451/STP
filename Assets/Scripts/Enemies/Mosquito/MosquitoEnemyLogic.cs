using UnityEngine;

public class MosquitoEnemyLogic : EnemyLogic
{

    private MosquitoEnemyMovement mosquitEnemyMovement;
    private MosquitoEnemyAttack mosquitEnemyAttack;


    void Start()
    {
        base.Start();
        mosquitEnemyMovement = GetComponent<MosquitoEnemyMovement>();
        mosquitEnemyAttack = GetComponent<MosquitoEnemyAttack>();
    }

    void Update()
    {
        if (player != null) base.Update();
    }

    protected override void CheckForStates()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > mosquitEnemyAttack.attackDistance)
        {
            currentStates = States.Chasing;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= mosquitEnemyAttack.attackDistance)
        {
            currentStates = States.Attack;
        }
    }

    protected override void Chase()
    {
        Debug.LogWarning("Mosquito is chasing");
        mosquitEnemyMovement.ChasePlayer();
    }

    protected override void Attack()
    {
        Debug.LogWarning("Masquito is doing range attack");
        isAttacking = true;
        mosquitEnemyMovement.StopChasing();
        StartCoroutine(mosquitEnemyAttack.RangeAttack()); ;
    }

    protected override void Death()
    {
        Debug.LogWarning("Mosquito is dying");
    }
}
