using System.Collections;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{

    public float attackDistance;
    [SerializeField] protected bool hasMeleeAttack;
    [SerializeField] protected bool hasRangeAttack;
    [SerializeField] protected float meleeAttackDistance;
    [SerializeField] protected float attackDelay; // the time after entering attack state and before firing projectile
    [SerializeField] protected float attackCooldown; // the time after firing projectile before checking next move
    protected Transform player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    public abstract IEnumerator RangeAttack();

    public abstract IEnumerator MeleeAttack();
}
