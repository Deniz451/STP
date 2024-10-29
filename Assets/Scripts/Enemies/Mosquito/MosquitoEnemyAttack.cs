using System;
using System.Collections;
using UnityEngine;

public class MosquitoEnemyAttack : EnemyAttack
{

    private Rigidbody rb;
    public Action OnAttackStart;


    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody>();
    }

    public override IEnumerator Attack(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(enemySO.attackDelay);
        OnAttackStart?.Invoke();

        Vector3 dashDirection = (targetPosition - transform.position).normalized;
        rb.AddForce(dashDirection * 800, ForceMode.Impulse);

        yield return new WaitForSeconds(enemySO.attackCooldown);
        OnAttackComplete?.Invoke();
    }
}
