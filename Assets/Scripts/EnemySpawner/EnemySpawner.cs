using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float spawnInterval;


    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartSpawning();
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
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            Vector3 randomPosition = GetRandomPointInRadius(randEnemy);
            Instantiate(enemyPrefabs[randEnemy], randomPosition, Quaternion.identity);
            Debug.Log(randomPosition);
        }
    }

    private Vector3 GetRandomPointInRadius(int enemyType)
    {
        Vector3 centerPosition = playerTransform != null ? playerTransform.position : transform.position;
        while (true)
        {
            Vector2 randomPoint2D = Random.insideUnitCircle * spawnRadius;
            Vector3 randomPosition = new Vector3(randomPoint2D.x, 0, randomPoint2D.y);

            switch (enemyType)
            {
                case 0:
                    randomPosition.y = 0.5f;
                    break;
                case 1:
                    randomPosition.y = 4f;
                    break;
                case 2:
                    randomPosition.y = 1.5f;
                    break;
            }

            if (Vector3.Distance(randomPosition, centerPosition) >= minSpawnDistance)
            {
                return randomPosition;
            }
        }
    }
}
