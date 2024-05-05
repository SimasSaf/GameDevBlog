using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public float spawnRate = 2f;
    public float spawnDistance = 10f;
    public Transform earthTransform;
    public int enemiesPerSpawn = 1;

    private bool spawnEnemies = false;
    private float nextSpawnTime = 0f;
    [SerializeField] private Transform enemyParentTransform;

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

    private void Awake()
    {
        GameObject enemiesParent = GameObject.Find("PoolManager/Enemies");
        enemyParentTransform = enemiesParent.transform;
    }

    void SpawnEnemy()
    {
        Vector3 spawnDirection = Random.onUnitSphere;
        Vector3 spawnPoint = earthTransform.position + spawnDirection * spawnDistance;
        GameObject enemyInstance = EnemyPoolManager.Instance.GetPooledEnemy();
        enemyInstance.transform.position = spawnPoint;
        enemyInstance.transform.LookAt(earthTransform.position);
        EnemyMovement enemyMovement = enemyInstance.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.target = earthTransform;
        }
        EnemyBehavior enemyBehavior = enemyInstance.GetComponent<EnemyBehavior>();
        if (enemyBehavior != null)
        {
            Enemy enemy = new Enemy(10, 5);
            enemyBehavior.Initialize(enemy);
        }
    }

    public void StartSpawningEnemies()
    {
        spawnEnemies = true;
    }

    public void StopSpawningEnemies()
    {
        Debug.Log("Stoping to spawn");
        spawnEnemies = false;
    }
}
