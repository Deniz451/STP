using UnityEngine;

public class SpiderEnemyLogic : EnemyLogic
{

    private SpiderEnemyMovement spiderEnemyMovement;
    private SpiderEnemyAttack spiderEnemyAttack;


    void Start()
    {
        base.Start();
        spiderEnemyMovement = GetComponent<SpiderEnemyMovement>();
        spiderEnemyAttack = GetComponent<SpiderEnemyAttack>();
    }

    void Update()
    {
        if (player != null) base.Update();
    }

    protected override void CheckForStates()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > spiderEnemyAttack.attackDistance)
        {
            currentStates = States.Chasing;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= spiderEnemyAttack.attackDistance)
        {
            currentStates = States.Attack;
        }
    }

    protected override void Chase()
    {
        Debug.LogWarning("Spider is chasing");
        spiderEnemyMovement.ChasePlayer();
    }

    protected override void Attack()
    {
        Debug.LogWarning("Spider is doing range attack");
        isAttacking = true;
        spiderEnemyMovement.StopChasing();
        StartCoroutine(spiderEnemyAttack.RangeAttack());
    }

    protected override void Death()
    {
        Debug.LogWarning("Spider is dying");
    }
}
