using System.Collections;
using UnityEngine;
using System;

public abstract class EnemyAttack : MonoBehaviour
{

    [SerializeField] protected EnemySO enemySO;
    protected Transform player;
    public Action OnAttackComplete;


    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform ?? player;
    }

    protected virtual void Update()
    {
        // tries to find the player if there was no initialy at the start
        if (player == null && GameObject.FindGameObjectWithTag("Player")) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public abstract IEnumerator Attack(Vector3 targetPosition);
}
