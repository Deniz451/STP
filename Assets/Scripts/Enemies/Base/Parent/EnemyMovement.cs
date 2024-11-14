using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    private EnemyReferences enemyReferences;
    private bool isChasing = false;

    protected virtual void Start()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    protected virtual void Update()
    {
        if (enemyReferences.playerTransform != null && isChasing)
        {
            LookAtPlayer();
            ChasePlayer();
        }
    }

    private void FixedUpdate()
    {
        if (isChasing)
        {
            Vector3 moveDir = (enemyReferences.playerTransform.position - transform.position).normalized;
            moveDir.y = 0;
            enemyReferences.rb.velocity = moveDir * enemyReferences.enemySO.moveSpeed;
        }
    }

    // gets called from the logic only once, after gets called every frame to update the playes position until said otherwise
    public void ChasePlayer()
    {
        if (enemyReferences.playerTransform != null) isChasing = true;
    }

    public void StopChasing()
    {
        isChasing = false;
    }

    private void LookAtPlayer()
    {
        Vector3 lookAt = enemyReferences.playerTransform.position;
        lookAt.y = 0;
        transform.LookAt(lookAt);
    }
}
