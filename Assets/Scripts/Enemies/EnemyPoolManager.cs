using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour, ILevelingSystemObserver
{
    public static EnemyPoolManager Instance;

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private int poolSize = 30;
    private bool increasedPoolLvl20 = false;

    private bool increasedPoolLvl10 = false;
    private bool increasedPoolLvl5 = false;

    [SerializeField]
    private Transform enemiesContainer;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private List<GameObject> activeEnemies = new List<GameObject>();
    private ILevelTracker levelTracker;

    private void Awake()
    {
        enemiesContainer = transform.Find("Enemies");
        levelTracker = FindAnyObjectByType<LevelingSystem>();
        Instance = this;
        InitializePool();
    }

    public GameObject GetPooledEnemy()
    {
        GameObject pooledEnemy;
        if (enemyPool.Count > 0)
        {
            pooledEnemy = enemyPool.Dequeue();
        }
        else
        {
            GameObject randomPrefab = RandomPrefab();
            pooledEnemy = Instantiate(randomPrefab, enemiesContainer);
        }
        pooledEnemy.SetActive(true);
        activeEnemies.Add(pooledEnemy);
        return pooledEnemy;
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        if (!increasedPoolLvl20)
        {
            if (levelTracker.getLevel() >= 20)
            {
                poolSize = 200;
                InitializePool();
                increasedPoolLvl20 = true;
            }
        }
        if (!increasedPoolLvl5)
        {
            if (levelTracker.getLevel() >= 5)
            {
                poolSize = 60;
                InitializePool();
                increasedPoolLvl5 = true;
            }
        }
        if (!increasedPoolLvl10)
        {
            if (levelTracker.getLevel() >= 10)
            {
                poolSize = 130;
                InitializePool();
                increasedPoolLvl10 = true;
            }
        }

        Debug.Log("Returning enemy to pool Manager");

        enemy.SetActive(false);
        if (!enemyPool.Contains(enemy))
        {
            enemyPool.Enqueue(enemy);
        }
        activeEnemies.Remove(enemy);
    }

    public void DeactivateAllEnemies()
    {
        while (activeEnemies.Count > 0)
        {
            GameObject enemy = activeEnemies[0];
            ReturnEnemyToPool(enemy);
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject randomPrefab = RandomPrefab();
            GameObject enemy = Instantiate(randomPrefab, enemiesContainer);

            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    private GameObject RandomPrefab()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    public bool HasAvailableEnemies()
    {
        return enemyPool.Count > 0;
    }

    public void OnLevelUp(int level) { }

    public void OnReset()
    {
        poolSize = 30;
        increasedPoolLvl5 = false;
        increasedPoolLvl10 = false;
        increasedPoolLvl20 = false;

        foreach (GameObject enemy in enemyPool)
        {
            Destroy(enemy);
        }
        enemyPool.Clear();

        foreach (GameObject enemy in activeEnemies)
        {
            Destroy(enemy);
        }
        activeEnemies.Clear();

        InitializePool();
    }

    public void OnAddExperience(int experience, int experienceToNextLevel) { }
}
