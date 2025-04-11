using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnerSettings settings;
    public GameInfo gameInfo;
    private Transform _player;
    private int _enemySpawned;
    private int _enemyKilled;
    private bool allEnemiesSpawned = false;


    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.WaveStart, SpawnEnemiesCaller);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.WaveStart, SpawnEnemiesCaller);
    }

    private void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void SpawnEnemiesCaller() { StartCoroutine(SpawnEnemies()); }

    private IEnumerator SpawnEnemies() {
        allEnemiesSpawned = false;
        for (int i = 0; i < gameInfo.waveCount * settings.spawnMultiplier; i++) {
           yield return new WaitForSeconds(settings.spawnInterval);
           GameObject enemy = Instantiate(settings.spiderEnemy, GetSpawnPos(), Quaternion.identity);
           enemy.GetComponent<IDamagable>().OnDeath += CheckForEnemiesKilled;
           _enemySpawned++;
        }
        allEnemiesSpawned = true;
    }

    private void CheckForEnemiesKilled() {
        _enemyKilled++;
         if (allEnemiesSpawned && _enemyKilled == _enemySpawned)
            EventManager.Instance.Publish(GameEvents.EventType.AllEnemiesKilled);
    }

    private Vector3 GetSpawnPos() {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float distance = Random.Range(settings.minSpawnDistance, settings.maxSpawnDistance);
        Vector3 offset = new Vector3(randomDir.x, 0f, randomDir.y) * distance;
        return _player.position + offset;
    }
}
