using System.Collections;
using UnityEngine;
using System;

public class testAttack : MonoBehaviour
{

    [SerializeField] private EnemySO spider;
    [SerializeField] private Transform projectileSpawnTransform;
    private Transform player;
    public Action OnAttackComplete;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform ?? player;
    }

    private void Update()
    {
        // tries to find the player if there was no initialy at the start
        if (player == null && GameObject.FindGameObjectWithTag("Player")) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public IEnumerator Attack(Vector3 targetPosition)
    {
        // waits delay before performing attack
        yield return new WaitForSeconds(spider.attackDelay);
        GameObject webProjectile = Instantiate(spider.projectilePrefab, projectileSpawnTransform.position, Quaternion.identity);
        webProjectile.GetComponent<SpiderProjectile>().targetPosition = targetPosition;

        yield return new WaitForSeconds(spider.attackCooldown);
        if (Vector3.Distance(transform.position, player.position) <= spider.attackDistance) StartCoroutine(Attack(player.position));

        OnAttackComplete?.Invoke();
    }
}
