using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int poolSize = 50;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = InstantiateRandomPrefab();
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    private GameObject InstantiateRandomPrefab()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject randomPrefab = enemyPrefabs[randomIndex];
        return Instantiate(randomPrefab);
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
            pooledEnemy = InstantiateRandomPrefab();
        }
        pooledEnemy.SetActive(true);
        activeEnemies.Add(pooledEnemy);
        return pooledEnemy;
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
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
}
