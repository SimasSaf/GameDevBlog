using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public float spawnRate = 2f; // Time between spawns
    public float spawnDistance = 10f; // Distance from the Earth to spawn enemies
    public Transform earthTransform; // Assign the Earth's transform
    public int enemiesPerSpawn = 1; // Number of enemies to spawn each time

    private bool spawnEnemies = false;
    private float nextSpawnTime = 0f; // Initialize nextSpawnTime

    void Update()
    {
        if (spawnEnemies && Time.time >= nextSpawnTime)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                SpawnEnemy();
            }
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnDirection = Random.onUnitSphere;
        Vector3 spawnPoint = earthTransform.position + spawnDirection * spawnDistance;

        // Use the pool manager to get an enemy
        GameObject enemyInstance = EnemyPoolManager.Instance.GetPooledEnemy();
        enemyInstance.transform.position = spawnPoint;
        enemyInstance.transform.LookAt(earthTransform.position);

        // Initialize or reset enemy movement and behavior as needed
        EnemyMovement enemyMovement = enemyInstance.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.target = earthTransform;
        }
        else
        {
            Debug.LogWarning("EnemyMovement component not found on the pooled enemy.");
        }

        // Assuming an Initialize method exists for setting up/resetting the enemy
        EnemyBehavior enemyBehavior = enemyInstance.GetComponent<EnemyBehavior>();
        if (enemyBehavior != null)
        {
            // Reset or initialize enemy behavior
            Enemy enemy = new Enemy(10, 5);
            enemyBehavior.Initialize(enemy);
        }
        else
        {
            Debug.LogWarning("EnemyBehavior component not found on the pooled enemy.");
        }
    }

    public void StartSpawningEnemies()
    {
        spawnEnemies = true;
    }

    public void StopSpawningEnemies()
    {
        spawnEnemies = false;
    }
}
