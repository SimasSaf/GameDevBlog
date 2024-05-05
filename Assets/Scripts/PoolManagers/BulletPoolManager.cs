using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 50;
    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        // Ensure that there's only one instance of the BulletPoolManager
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        // Instantiate and store bullets in the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetPooledBullet()
    {
        // Check if there are bullets available in the pool
        if (bulletPool.Count > 0)
        {
            GameObject pooledBullet = bulletPool.Dequeue();
            pooledBullet.SetActive(true);
            return pooledBullet;
        }
        else
        {
            // Optionally expand the pool if all bullets are in use
            GameObject newBullet = Instantiate(bulletPrefab);
            newBullet.SetActive(true);
            return newBullet;
        }
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
