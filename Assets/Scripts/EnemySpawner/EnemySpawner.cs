using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float spawnInterval;
    [SerializeField] private TextMeshProUGUI waveText;

    private int currentWave = 0;
    private int enemiesToSpawn;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            DisplayWaveText();

            enemiesToSpawn = Mathf.FloorToInt(currentWave * 1.5f + 5);
            yield return StartCoroutine(SpawnWaveEnemies());

            yield return new WaitUntil(() => activeEnemies.Count == 0);
        }
    }

    private void DisplayWaveText()
    {
        waveText.text = $"Wave {currentWave}";
        waveText.gameObject.SetActive(true);
    }

    private IEnumerator SpawnWaveEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyType = GetEnemyTypeForWave(currentWave);
            Vector3 spawnPosition = GetRandomPointInRadius(enemyType);
            GameObject enemy = Instantiate(enemyPrefabs[enemyType], spawnPosition, Quaternion.identity);
            activeEnemies.Add(enemy);

            enemy.GetComponent<EnemyHealth>().OnDeath += () => activeEnemies.Remove(enemy);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private int GetEnemyTypeForWave(int wave)
    {
        float randomValue = Random.Range(0f, 1f);

        if (wave >= 10 && randomValue > 0.5f) return 2;
        if (wave >= 5 && randomValue > 0.3f) return 1;
        return 0;
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
