using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Spawner Settings", fileName = "New Enemy Spawner Settings")]
public class EnemySpawnerSettings : ScriptableObject 
{
    [Header("Settings")]
    public float spawnMultiplier;
    public float minSpawnDistance;
    public float maxSpawnDistance;
    public float spawnInterval;

    [Space(30)]

    [Header("Enemies")]
    public GameObject spiderEnemy;
    public GameObject mosquitoEnemy;
    public GameObject woodlouseEnemy;
}
