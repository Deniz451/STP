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
        // Wait a delay
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

        // Waits a time cooldown
        yield return new WaitForSeconds(enemyReferences.enemySO.attackCooldown);

        // If player still in range, repeats attack, else invokes attack completion
        if (enemyReferences.playerTransform != null && Vector3.Distance(transform.position, enemyReferences.playerTransform.position) <= enemyReferences.enemySO.attackDistance) 
            StartCoroutine(Attack(enemyReferences.playerTransform.position));
        else
            OnAttackComplete?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamagable>().TakeDamage(enemyReferences.enemySO.damage);
        }
    }
}
