using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int poolSize = 10;
    private Queue<GameObject> enemyPool = new Queue<GameObject>();

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
        if (enemyPool.Count > 0)
        {
            GameObject pooledEnemy = enemyPool.Dequeue();
            pooledEnemy.SetActive(true);
            return pooledEnemy;
        }
        else
        {
            // Optionally expand the pool if all enemies are in use
            GameObject newEnemy = InstantiateRandomPrefab();
            newEnemy.SetActive(true);
            return newEnemy;
        }
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
