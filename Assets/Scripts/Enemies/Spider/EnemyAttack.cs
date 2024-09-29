using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private Transform player;
    [SerializeField] private GameObject webProjectilePreafb;
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private float attackDelay;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public IEnumerator RangeAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        GameObject webProjectile = Instantiate(webProjectilePreafb, projectileSpawnTransform.position, Quaternion.identity);
        webProjectile.GetComponent<SpiderProjectile>().targetPosition = player.position;
    }
}
