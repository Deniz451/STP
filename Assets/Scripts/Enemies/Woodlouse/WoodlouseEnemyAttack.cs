using System.Collections;
using UnityEngine;

public class WoodlouseEnemyAttack : EnemyAttack
{
    public override IEnumerator Attack(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(enemyReferences.enemySO.attackDelay);
        Debug.Log("Attacked");

        yield return new WaitForSeconds(enemyReferences.enemySO.attackCooldown);
        if (Vector3.Distance(transform.position, enemyReferences.playerTransform.position) <= enemyReferences.enemySO.attackDistance) StartCoroutine(Attack(enemyReferences.playerTransform.position));

        OnAttackComplete?.Invoke();
    }
}
