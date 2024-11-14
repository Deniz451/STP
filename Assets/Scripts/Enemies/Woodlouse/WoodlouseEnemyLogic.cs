using UnityEngine;

public class WoodlouseEnemyLogic : EnemyLogic
{
    private WoodlouseEnemyMovement woodlouseEnemyMovement;
    private WoodlouseEnemyAttack woodlouseEnemyAttack;
    private WoodlouseEnemyHealth woodlouseEnemyHealth;


    protected override void Start()
    {
        base.Start();

        woodlouseEnemyMovement = GetComponent<WoodlouseEnemyMovement>();
        woodlouseEnemyAttack = GetComponent<WoodlouseEnemyAttack>();
        woodlouseEnemyHealth = GetComponent<WoodlouseEnemyHealth>();

        woodlouseEnemyAttack.OnAttackComplete += CompletedAttack;
        woodlouseEnemyHealth.OnDeath += Death;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Chase()
    {
        woodlouseEnemyMovement.ChasePlayer();
    }

    protected override void Attack()
    {
        isAttacking = true;
        woodlouseEnemyMovement.StopChasing();

        StartCoroutine(woodlouseEnemyAttack.Attack(enemyReferences.playerTransform.position));
    }

    protected override void Death()
    {
        base.Death();
        woodlouseEnemyMovement.StopChasing();
        woodlouseEnemyMovement.StopAllCoroutines();
        woodlouseEnemyAttack.StopAllCoroutines();
        StopAllCoroutines();
    }
}
