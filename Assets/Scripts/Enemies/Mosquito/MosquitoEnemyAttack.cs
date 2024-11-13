using System;
using System.Collections;
using UnityEngine;

public class MosquitoEnemyAttack : EnemyAttack
{

    private Rigidbody rb;
    public Action OnAttackStart;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackForce;
    private float elapsedAttackTime;


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

        while (elapsedAttackTime <= attackDuration)
        {
            Debug.Log("Attacking");
            elapsedAttackTime += Time.deltaTime;
            rb.AddForce(dashDirection * attackForce, ForceMode.Force);
            yield return null;
        }

        yield return new WaitForSeconds(enemySO.attackCooldown);
        OnAttackComplete?.Invoke();
        elapsedAttackTime = 0;
    }
}
