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
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
}
