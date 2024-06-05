using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private int poolSize = 50;

    [SerializeField]
    private Transform enemiesContainer;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Awake()
    {
        enemiesContainer = transform.Find("Enemies");

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
}
