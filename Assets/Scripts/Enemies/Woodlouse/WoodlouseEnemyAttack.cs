using System.Collections;
using UnityEngine;

public class WoodlouseEnemyAttack : EnemyAttack
{

    private float elapsedAttackTime;


    public override IEnumerator Attack(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(enemyReferences.enemySO.attackDelay);

        transform.LookAt(enemyReferences.playerTransform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        // Performe dash into player
        Vector3 dashDirection = (targetPosition - transform.position).normalized;
        float maxDashSpeed = enemyReferences.enemySO.maxDashSpeed; // Define a max speed

        while (elapsedAttackTime <= enemyReferences.enemySO.dashAttackDuration)
        {
            elapsedAttackTime += Time.deltaTime;
            enemyReferences.rb.AddForce(dashDirection * enemyReferences.enemySO.dashAttackForce, ForceMode.Force);

            // Cap velocity
            if (enemyReferences.rb.velocity.magnitude > maxDashSpeed)
            {
                enemyReferences.rb.velocity = enemyReferences.rb.velocity.normalized * maxDashSpeed;
            }

            yield return null;
        }

        enemyReferences.rb.velocity = Vector3.zero;
        elapsedAttackTime = 0;

        yield return new WaitForSeconds(enemyReferences.enemySO.attackCooldown);

        if (enemyReferences.playerTransform != null && Vector3.Distance(transform.position, enemyReferences.playerTransform.position) <= enemyReferences.enemySO.attackDistance) 
            StartCoroutine(Attack(enemyReferences.playerTransform.position));
        else
            OnAttackComplete?.Invoke();
    }
}
