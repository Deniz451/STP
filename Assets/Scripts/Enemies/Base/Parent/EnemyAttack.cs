using System.Collections;
using UnityEngine;
using System;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyReferences enemyReferences;
    public Action OnAttackComplete;

    protected virtual void Start()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    public abstract IEnumerator Attack(Vector3 targetPosition);
}
