using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float spawnInterval;


    private void Awake()
    {
        GameObject.Find("StartBtn").GetComponent<StartBtn>().onGameStart += StartSpawning;
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector3 randomPosition = GetRandomPointInRadius();
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randEnemy], randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPointInRadius()
    {
        Vector3 centerPosition = playerTransform != null ? playerTransform.position : transform.position;
        while (true)
        {
            Vector2 randomPoint2D = Random.insideUnitCircle * spawnRadius;
            Vector3 randomPosition = new Vector3(randomPoint2D.x, 0, randomPoint2D.y) + centerPosition;

            if (Vector3.Distance(randomPosition, centerPosition) >= minSpawnDistance)
            {
                return randomPosition;
            }
        }
    }
}
