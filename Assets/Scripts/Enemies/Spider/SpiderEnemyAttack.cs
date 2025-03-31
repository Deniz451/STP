using System.Collections;
using UnityEngine;

public class SpiderEnemyAttack : EnemyAttack
{
    [SerializeField] protected Transform projectileSpawnTransform;

    public override IEnumerator Attack(Vector3 targetPosition)
    {
        // Waits delay before performing attack
        yield return new WaitForSeconds(enemyReferences.enemySO.attackDelay);

        transform.LookAt(enemyReferences.playerTransform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        // Spawn the projectile
        GameObject webProjectile = Instantiate(enemyReferences.enemySO.projectilePrefab, projectileSpawnTransform.position, Quaternion.identity);
        webProjectile.GetComponent<SpiderProjectile>().targetPosition = targetPosition;

        // Wait another time cooldown
        yield return new WaitForSeconds(enemyReferences.enemySO.attackCooldown);

        // If player still in distance, repeat attack, else invoke attack completion
        if (enemyReferences.playerTransform != null && Vector3.Distance(transform.position, enemyReferences.playerTransform.position) <= enemyReferences.enemySO.attackDistance) 
            StartCoroutine(Attack(enemyReferences.playerTransform.position + new Vector3(-3.6f, 2.2f, 0f)));
        else
            OnAttackComplete?.Invoke();
    }
}
