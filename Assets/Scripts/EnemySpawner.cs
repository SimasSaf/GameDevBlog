using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Assign your enemy prefabs in the inspector
    public float spawnRate = 2f; // Time between spawns
    public float spawnDistance = 10f; // Distance from the Earth to spawn enemies
    public Transform earthTransform; // Assign the Earth's transform in the inspector
    public int enemiesPerSpawn = 1; // Number of enemies to spawn each time

    private bool spawnEnemies = false;
    private float nextSpawnTime = 0f; // Initialize nextSpawnTime

    void Update()
    {
        // Check if it's time to spawn the next enemy/enemies
        if (spawnEnemies && Time.time >= nextSpawnTime)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                SpawnEnemy();
            }
            // Update nextSpawnTime to the current time plus the spawn rate
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnDirection = Random.onUnitSphere;
        Vector3 spawnPoint = earthTransform.position + spawnDirection * spawnDistance;

        GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject enemyInstance = Instantiate(selectedEnemyPrefab, spawnPoint, Quaternion.identity);

        EnemyMovement enemyMovement = enemyInstance.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.target = earthTransform;
        }
        else
        {
            Debug.LogWarning("EnemyMovement component not found on the instantiated enemy. Ensure it is attached.");
        }

        enemyInstance.transform.LookAt(earthTransform.position);
    }

    public void StartSpawningEnemies()
    {
        spawnEnemies = true;
    }
}
