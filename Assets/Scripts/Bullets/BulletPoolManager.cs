using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour, IBulletPoolManger
{
    public static BulletPoolManager Instance;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private int poolSize = 50;

    [SerializeField]
    private Transform bulletsContainer;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    private void Awake()
    {
        bulletsContainer = transform.Find("Bullets");

        if (bulletsContainer)
        {
            InitializePool();
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletsContainer);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetPooledBullet()
    {
        if (bulletPool.Count <= 0)
        {
            Debug.Log("Too many bullets, dont have more in pool. Making more...");
            InitializePool();
        }

        GameObject pooledBullet = bulletPool.Dequeue();
        pooledBullet.SetActive(true);
        return pooledBullet;
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
