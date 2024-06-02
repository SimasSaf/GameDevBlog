using UnityEngine;

public class EnemySpawnManager : MonoBehaviour, ILevelingSystemObserver
{
    [SerializeField]
    private int health;

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

    private ILevelTracker levelTracker;

    private void Awake()
    {
        GameObject enemiesParent = GameObject.Find("PoolManager/Enemies");
        enemyParentTransform = enemiesParent.transform;
        earthTransform = GameObject.Find("Earth").transform;
        levelTracker = FindAnyObjectByType<LevelingSystem>();

        if (levelTracker is LevelingSystem levelingSystem)
        {
            levelingSystem.RegisterObserver(this);
        }
    }

    private void OnDestroy()
    {
        if (levelTracker is LevelingSystem levelingSystem)
        {
            levelingSystem.UnregisterObserver(this);
        }
    }

    private void Start()
    {
        OnReset();
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
                enemyMovement.speed = speed;
            }

            EnemyBehavior enemyBehavior = enemyInstance.GetComponent<EnemyBehavior>();
            if (enemyBehavior != null)
            {
                int enemyHealth = health + (10 * levelTracker.getLevel());
                float enemySpeed = speed + (0.1f * levelTracker.getLevel());
                Enemy enemy = new Enemy(enemyHealth, enemySpeed);
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

    // ILevelingSystemObserver implementation
    public void OnLevelUp(int newLevel) { }

    public void OnReset()
    {
        health = 30;
        speed = 0.2f;
        spawnRate = 10f;
        spawnDistanceMin = 0f;
        spawnDistanceMax = 10f;
        enemiesPerSpawn = 30;
        nextSpawnTime = 1f;
    }

    public void OnAddExperience(int experience, int experienceToNextLevel) { }
}
