using System.Collections;
using UnityEngine;

public class WoodlouseEnemyAttack : EnemyAttack
{

    public override IEnumerator Attack(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(enemySO.attackDelay);
        Debug.Log("Attacked");

        yield return new WaitForSeconds(enemySO.attackCooldown);
        if (Vector3.Distance(transform.position, player.position) <= enemySO.attackDistance) StartCoroutine(Attack(player.position));

        OnAttackComplete?.Invoke();
    }
}
