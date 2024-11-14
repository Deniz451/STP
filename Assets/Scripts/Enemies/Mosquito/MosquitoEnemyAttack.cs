using System;
using System.Collections;
using UnityEngine;

public class MosquitoEnemyAttack : EnemyAttack
{
    public Action OnAttackStart; // Why does this exist - remove?
    private float elapsedAttackTime;


    protected override void Start()
    {
        base.Start();
    }

    public override IEnumerator Attack(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(enemyReferences.enemySO.attackDelay);

        Vector3 dashDirection = (targetPosition - transform.position).normalized;
        while (elapsedAttackTime <= enemyReferences.enemySO.dashAttackDuration)
        {
            Debug.Log("Attacking");
            elapsedAttackTime += Time.deltaTime;
            enemyReferences.rb.AddForce(dashDirection * enemyReferences.enemySO.dashAttackForce, ForceMode.Force);
            yield return null;
        }
        enemyReferences.rb.velocity = Vector3.zero;
        elapsedAttackTime = 0;

        yield return new WaitForSeconds(enemyReferences.enemySO.attackCooldown);
        if (Vector3.Distance(transform.position, enemyReferences.playerTransform.position) <= enemyReferences.enemySO.attackDistance) StartCoroutine(Attack(enemyReferences.playerTransform.position));
        OnAttackComplete?.Invoke();
    }
}
