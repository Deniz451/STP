using System;
using System.Collections;
using UnityEngine;

public class MosquitoEnemyAttack : EnemyAttack
{
    public Action OnAttackStart;
    private float elapsedAttackTime;


    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator Attack(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(enemyReferences.enemySO.attackDelay);
        OnAttackStart?.Invoke();

        Vector3 dashDirection = (targetPosition - transform.position).normalized;

        while (elapsedAttackTime <= enemyReferences.enemySO.dashAttackDuration)
        {
            Debug.Log("Attacking");
            elapsedAttackTime += Time.deltaTime;
            enemyReferences.rb.AddForce(dashDirection * enemyReferences.enemySO.dashAttackForce, ForceMode.Force);
            yield return null;
        }

        yield return new WaitForSeconds(enemyReferences.enemySO.attackCooldown);
        OnAttackComplete?.Invoke();
        elapsedAttackTime = 0;
    }
}
