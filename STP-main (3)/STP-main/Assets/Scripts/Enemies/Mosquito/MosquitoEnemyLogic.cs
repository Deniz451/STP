using UnityEngine;

public class MosquitoEnemyLogic : EnemyLogic
{
    private MosquitoEnemyMovement mosquitoEnemyMovement;
    private MosquitoEnemyAttack mosquitoEnemyAttack;
    private MosquitoEnemyHealth mosquitoEnemyHealth;


    protected override void Start()
    {
        base.Start();

        mosquitoEnemyMovement = GetComponent<MosquitoEnemyMovement>();
        mosquitoEnemyAttack = GetComponent<MosquitoEnemyAttack>();
        mosquitoEnemyHealth = GetComponent<MosquitoEnemyHealth>();

        mosquitoEnemyAttack.OnAttackComplete += CompletedAttack;
        mosquitoEnemyHealth.OnDeath += Death;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Chase()
    {
        mosquitoEnemyMovement.ChasePlayer();
    }

    protected override void Attack()
    {
        isAttacking = true;
        mosquitoEnemyMovement.StopChasing();

        StartCoroutine(mosquitoEnemyAttack.Attack(enemyReferences.playerTransform.position + new Vector3(-3.6f, 2.2f, 0f)));
    }

    protected override void Death()
    {
        base.Death();
        mosquitoEnemyMovement.StopChasing();
        mosquitoEnemyMovement.StopAllCoroutines();
        mosquitoEnemyAttack.StopAllCoroutines();
        StopAllCoroutines();
    }

}
