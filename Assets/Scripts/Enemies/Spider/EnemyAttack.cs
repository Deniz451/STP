using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float attackDistance;
    [SerializeField] private float meleeAttackDistance;
    [SerializeField] private float attackDelay; // the time after entering attack state and before firing projectile
    [SerializeField] private float attackCooldown; // the time after firing projectile before checking next move
    [SerializeField] private GameObject webProjectilePreafb;
    [SerializeField] private Transform projectileSpawnTransform;
    private Transform player;
    private EnemyLogic enemyLogic;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyLogic = GetComponent<EnemyLogic>();
    }

    public IEnumerator RangeAttack()
    {
        // waits delay before performing attack
        yield return new WaitForSeconds(attackDelay);


        //if player moved away, get closer and repeat range attack
        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            enemyLogic.SetAttackBoolFalse();
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
            enemyLogic.SetAttackBoolFalse();
        }
    }

    public IEnumerator MeleeAttack()
    {
        yield return new WaitForSeconds(attackDelay);
    }
}
