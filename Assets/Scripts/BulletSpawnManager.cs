using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnManager : MonoBehaviour
{
    public static BulletSpawnManager Instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private int poolSize = 50;
    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    private Transform bulletParentTransform; // Transform to hold the pooled bullets (in the hierarchy)


    public float bulletSpeed = 20f;
    public float fireRate = 1f; // Bullets per second
    private int shouldSplit = 0; // Determines if and how many times the bullet splits
    private bool isLaser = false; // Determines if the bullet is a laser
    private bool isOnFire = false; // Determines if the bullet is on fire

    private float nextFireTime = 0f; // When the next shot is due

    private void Awake()
    {
        Instance = this; // Assign the static Instance to this instance of BulletManager
        InitializePool();

        GameObject bulletsParent = GameObject.Find("PoolManager/Bullets");
        
        bulletParentTransform = bulletsParent.transform;
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullet.transform.SetParent(bulletParentTransform, false); // For organizing bullets in the "hierarchy"
            bulletPool.Enqueue(bullet);
        }
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootBullet()
    {
        GameObject bulletInstance = GetPooledBullet();
        bulletInstance.transform.position = shootingPoint.position;
        bulletInstance.transform.rotation = shootingPoint.rotation;
        Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            Vector2 forceDirection = -shootingPoint.up.normalized * bulletSpeed;
            bulletRb.AddForce(forceDirection, ForceMode2D.Impulse);
        }

        // Assuming BulletBehavior handles its own properties like isOnFire, isLaser, shouldSplit
        BulletBehavior bulletBehavior = bulletInstance.GetComponent<BulletBehavior>();

        Bullet bullet = new Bullet(isOnFire, isLaser, shouldSplit);

        if (bulletBehavior != null)
        {
            bulletBehavior.Initialize(bullet);
        }
    }

    private GameObject GetPooledBullet()
    {
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
            newBullet.SetActive(false); // It will be set to active in ShootBullet()
            return newBullet;
        }
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    // Setters for encapsulation
    public void SetIsLaser(bool isLaser)
    {
        this.isLaser = isLaser;
    }

    public void SetIsOnFire(bool isOnFire)
    {
        this.isOnFire = isOnFire;
    }

    public void SetShouldSplit(int shouldSplit)
    {
        this.shouldSplit = shouldSplit;
    }
}
