using System.Collections;
using UnityEngine;

public class SpiderEnemyAttack : EnemyAttack
{

    [SerializeField] protected Transform projectileSpawnTransform;


    public override IEnumerator Attack(Vector3 targetPosition)
    {
        // waits delay before performing attack
        yield return new WaitForSeconds(enemySO.attackDelay);
        GameObject webProjectile = Instantiate(enemySO.projectilePrefab, projectileSpawnTransform.position, Quaternion.identity);
        webProjectile.GetComponent<SpiderProjectile>().targetPosition = targetPosition;

        yield return new WaitForSeconds(enemySO.attackCooldown);
        if (Vector3.Distance(transform.position, player.position) <= enemySO.attackDistance) StartCoroutine(Attack(player.position));

        OnAttackComplete?.Invoke();
    }
}
