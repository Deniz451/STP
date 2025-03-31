using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float minSpawnDistance = 3f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private TextMeshProUGUI waveText;

    private int currentWave = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            DisplayWaveText();

            int enemiesToSpawn = Mathf.FloorToInt(currentWave * 1.5f + 5);
            yield return StartCoroutine(SpawnWaveEnemies(enemiesToSpawn));

            yield return new WaitUntil(() => activeEnemies.Count == 0);
        }
    }

    private void DisplayWaveText()
    {
        waveText.text = $"Wave {currentWave}";
        waveText.gameObject.SetActive(true);
    }

    private IEnumerator SpawnWaveEnemies(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyType = Random.Range(0, enemyPrefabs.Length); // Pick a random enemy type
            Vector3 spawnPosition = GetRandomPointInRadius();

            GameObject enemy = Instantiate(enemyPrefabs[enemyType], spawnPosition, Quaternion.identity);
            activeEnemies.Add(enemy);

            enemy.GetComponent<EnemyHealth>().OnDeath += () => activeEnemies.Remove(enemy);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomPointInRadius()
    {
        Vector3 center = playerTransform ? playerTransform.position : transform.position;
        Vector3 spawnPosition;

        do
        {
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            spawnPosition = new Vector3(center.x + randomCircle.x, 0, center.z + randomCircle.y);
        }
        while (Vector3.Distance(spawnPosition, center) < minSpawnDistance); // Avoid spawning too close

        return spawnPosition;
    }
}
