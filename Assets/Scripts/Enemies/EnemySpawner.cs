using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // komumnikuje s wave managerem
    // wave manager rika kdy spawnovat
    // vybira nahodnou lokaci okolo areny (zalezi na pozici ostatnich nepratel?)
    // nepratele oznaceni 1-3
    // zakladni spawn cislo pro kazfz oznaceni nasobene koefientem wavu a obtiznosti
    // spawnuje jednotlive po danym intervalu

    [SerializeField] private GameObject[] enemiesType1;
    [SerializeField] private GameObject[] enemiesType2;
    [SerializeField] private GameObject[] enemiesType3;
    [SerializeField] private float baseSpawnNumberType1;
    [SerializeField] private float baseSpawnNumberType2;
    [SerializeField] private float baseSpawnNumberType3;
    [SerializeField] private Transform[] spawnLocations;

    public int difficulty = 2; // passed from another script

    public void SpawnEnemies(float waveNumber)
    {
        float waveFactor = waveNumber * 1.4f;
        float difficultyFactor = difficulty * 1.5f;

        int totalSpawnType1 = (int)Mathf.Round(baseSpawnNumberType1 * waveFactor * difficultyFactor);
        int totalSpawnType2 = (int)Mathf.Round(baseSpawnNumberType2 * waveFactor * difficultyFactor);
        int totalSpawnType3 = (int)Mathf.Round(baseSpawnNumberType3 * waveFactor * difficultyFactor);

        if (totalSpawnType2 == 0)
        {
            baseSpawnNumberType2 += 0.2f;
        }
        if (totalSpawnType3 == 0)
        {
            baseSpawnNumberType2 += 0.1f;
        }

        Debug.Log("Wave " + waveNumber + "!");
        Debug.Log("Spawning " + totalSpawnType1 + " type 1 enemies.");
        Debug.Log("Spawning " + totalSpawnType2 + " type 1 enemies.");
        Debug.Log("Spawning " + totalSpawnType3 + " type 1 enemies.");
    }

    private void Start()
    {
        SpawnEnemies(1);
    }
}
