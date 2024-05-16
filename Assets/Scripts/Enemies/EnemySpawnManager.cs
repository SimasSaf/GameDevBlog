using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private int health = 30;

    [SerializeField]
    private float speed;
    public float spawnRate;
    private float spawnDistanceMin;
    private float spawnDistanceMax;
    public Transform earthTransform;
    public int enemiesPerSpawn;
    private bool spawnEnemies = false;
    private float nextSpawnTime;

    [SerializeField]
    private Transform enemyParentTransform;

    private void Awake()
    {
        GameObject enemiesParent = GameObject.Find("PoolManager/Enemies");
        enemyParentTransform = enemiesParent.transform;
        earthTransform = GameObject.Find("Earth").transform;
    }

    private void Start()
    {
        speed = 0.5f;
        spawnRate = 10f;
        spawnDistanceMin = 0f;
        spawnDistanceMax = 10f;
        enemiesPerSpawn = 30;
        nextSpawnTime = 1f;
    }

    void Update()
    {
        if (spawnEnemies && Time.time >= nextSpawnTime)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyPoolManager.Instance.HasAvailableEnemies())
                {
                    SpawnEnemy();
                }
            }
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        float randomDistance = Random.Range(spawnDistanceMin, spawnDistanceMax);
        Vector3 spawnPoint = GetSpawnPointBeyondScreenBounds(randomDistance);

        GameObject enemyInstance = EnemyPoolManager.Instance.GetPooledEnemy();
        if (enemyInstance != null)
        {
            enemyInstance.transform.position = spawnPoint;
            enemyInstance.transform.LookAt(earthTransform.position);

            EnemyMovement enemyMovement = enemyInstance.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.target = earthTransform;
                Debug.Log("Setting speed");
                enemyMovement.speed = speed;
            }

            EnemyBehavior enemyBehavior = enemyInstance.GetComponent<EnemyBehavior>();
            if (enemyBehavior != null)
            {
                Enemy enemy = new Enemy(health, speed);
                enemyBehavior.Initialize(enemy);
            }
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

    Vector3 GetSpawnPointBeyondScreenBounds(float distance)
    {
        Camera mainCamera = Camera.main;

        float cameraToTargetDistance = (
            earthTransform.position - mainCamera.transform.position
        ).magnitude;

        Vector3 topRightScreenBound = mainCamera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, cameraToTargetDistance)
        );

        Vector3 spawnDirection = Random.insideUnitCircle.normalized;
        spawnDirection.z = 0;

        Vector3 spawnPoint =
            earthTransform.position
            + spawnDirection
                * (Vector3.Distance(earthTransform.position, topRightScreenBound) + distance);

        spawnPoint.z = 0;

        return spawnPoint;
    }
}
