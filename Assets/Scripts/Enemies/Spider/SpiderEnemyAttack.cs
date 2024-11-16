using System.Collections;
using UnityEngine;

public class SpiderEnemyAttack : EnemyAttack
{
    [SerializeField] protected Transform projectileSpawnTransform;


    public override IEnumerator Attack(Vector3 targetPosition)
    {
        // waits delay before performing attack
        yield return new WaitForSeconds(enemyReferences.enemySO.attackDelay);
        GameObject webProjectile = Instantiate(enemyReferences.enemySO.projectilePrefab, projectileSpawnTransform.position, Quaternion.identity);
        webProjectile.GetComponent<SpiderProjectile>().targetPosition = targetPosition;

        yield return new WaitForSeconds(enemyReferences.enemySO.attackCooldown);
        if (enemyReferences.playerTransform != null && Vector3.Distance(transform.position, enemyReferences.playerTransform.position) <= enemyReferences.enemySO.attackDistance) StartCoroutine(Attack(enemyReferences.playerTransform.position));

        OnAttackComplete?.Invoke();
    }
}
