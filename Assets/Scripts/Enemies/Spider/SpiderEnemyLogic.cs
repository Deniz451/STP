using UnityEngine;

public class SpiderEnemyLogic : EnemyLogic
{
    private SpiderEnemyMovement spiderEnemyMovement;
    private SpiderEnemyAttack spiderEnemyAttack;
    private SpiderEnemyHealth spiderEnemyHealth;


    protected override void Start()
    {
        base.Start();

        spiderEnemyMovement = GetComponent<SpiderEnemyMovement>();
        spiderEnemyAttack = GetComponent<SpiderEnemyAttack>();
        spiderEnemyHealth = GetComponent<SpiderEnemyHealth>();

        spiderEnemyAttack.OnAttackComplete += CompletedAttack;
        spiderEnemyHealth.OnDeath += Death;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Chase()
    {
        spiderEnemyMovement.ChasePlayer();
    }

    protected override void Attack()
    {
        isAttacking = true;
        spiderEnemyMovement.StopChasing();

        StartCoroutine(spiderEnemyAttack.Attack(enemyReferences.playerTransform.position + new Vector3(-3.6f, 2.2f, 0f)));
    }

    protected override void Death()
    {
        base.Death();
        spiderEnemyMovement.StopChasing();
        spiderEnemyMovement.StopAllCoroutines();
        spiderEnemyAttack.StopAllCoroutines();
        StopAllCoroutines();
    }

}
