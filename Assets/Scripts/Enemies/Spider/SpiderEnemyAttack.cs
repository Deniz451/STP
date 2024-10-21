using System.Collections;
using UnityEngine;

public class SpiderEnemyAttack : EnemyAttack
{

    [SerializeField] private GameObject webProjectilePreafb;
    [SerializeField] private Transform projectileSpawnTransform;
    private SpiderEnemyLogic spiderEnemyLogic;

    public override IEnumerator RangeAttack()
    {
        if (hasRangeAttack)
        {
            // waits delay before performing attack
            yield return new WaitForSeconds(attackDelay);


            //if player moved away, get closer and repeat range attack
            if (Vector3.Distance(transform.position, player.position) > attackDistance)
            {
                spiderEnemyLogic.SetAttackBoolFalse();
            }
            else
            {
                // spawns projectle
                GameObject webProjectile = Instantiate(webProjectilePreafb, projectileSpawnTransform.position, Quaternion.identity);
                webProjectile.GetComponent<SpiderProjectile>().targetPosition = player.position;
            }


            // waits cooldown before making next move
            yield return new WaitForSeconds(attackCooldown);


            // if player didnt move, get closer for melee attack
            if (Vector3.Distance(transform.position, player.position) <= attackDistance)
            {
                // move closer for melee
            }
            //if player moved away, get closer and repeat range attack
            else if (Vector3.Distance(transform.position, player.position) > attackDistance)
            {
                spiderEnemyLogic.SetAttackBoolFalse();
            }
        }
    }

    public override IEnumerator MeleeAttack()
    {
        if (hasMeleeAttack)
        {
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
