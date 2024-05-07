using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //List of groups of enemies to spawn this wave
        public int waveQuota; //Total number of enemies to spawn this wave
        public float spawnInterval; //Interval at which to spawn enemies
        public int spawnCount; //Number of enemies already spawned this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string tag;
        public int enemyCount; //Number of enemies of this type to spawn this wave
        public int spawnCount; //Number of enemies of this type already spawned this wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; //List of all waves in the game
    public int currentWaveCount; //index of the current wave

    EnemyPooler enemyPooler;

    [Header("Spawner Attributes")]
    float spawnTimer; // Timer used to determine when to spawn next enemy
    public int enemiesAlive;
    public int maxEnemiesAllowed; // Maximum number of enemies allowed on map at once
    public bool maxEnemiesReached = false; // Indicates if max number of enemies has been reached
    public float waveInterval; // Interval between each wave
    bool isWaveActive = false;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; // List to store relative spawn points of enemies

    private Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        enemyPooler = FindObjectOfType<EnemyPooler>();
        CalculateWaveQuota();
    }

    void Update()
    {
        // Check if wave has ended and the next wave should start
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive)
        {
            StartCoroutine(BeginNextWave());
        }
        spawnTimer += Time.deltaTime;
        // Check if time to spawn next enemy
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    private IEnumerator BeginNextWave()
    {
        isWaveActive = true;
        // Wait for "waveInterval" seconds before starting the next wave
        yield return new WaitForSeconds(waveInterval);
        // If there are more waves after the current wave, move to next wave
        if (currentWaveCount < waves.Count - 1)
        {
            isWaveActive = false;
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;

        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    /// <summary>
    ///  This method will stop spawning enemies if the amount of enemies on the map is maximum.
    ///  The method will only spawn enemies in a particular wave until it is time for the next wave's enemies to be spawned
    /// </summary>
    void SpawnEnemies()
    {
        // Check if the minimum number of enemies in the wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            // Spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // Check if the minimum number of enemies of this type have been spawned
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    // Spawn the enemy at a random position close to the player from the pool of enemies
                    enemyPooler.SpawnFromPool(enemyGroup.tag, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;

                    // Limit number of enemies that can be spawned at once
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                }
            }
        }
    }

    // Call this function when an enemy is killed
    public void OnEnemyKilled()
    {
        enemiesAlive--;

        // Reset the maxEnemiesReached flag if the number of enemies alive has dropped below the maximum allowed
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }
}
