using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootingPoint;

    public float bulletSpeed = 20f;
    public float fireRate = 1f; // Bullets per second
    private int shouldSplit = 0; // 0 means doesnt split, 1 splits one time, 2 splits two times
    private bool isLaser = false; // is not a laser but a bullet
    private bool isOnFire = false; // bullet is not on fire


    private float nextFireTime = 0f; // When the next shot is due

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootBullet();
            // Set the next fire time by adding the inverse of the fire rate to the current time
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootBullet()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            Vector2 forceDirection = -shootingPoint.up.normalized * bulletSpeed;
            bulletRb.AddForce(forceDirection, ForceMode2D.Impulse);
        }

        // Create a new Bullet object with the given parameters
        Bullet bullet = new Bullet(isOnFire, isLaser, shouldSplit);

        // Directly initialize the BulletBehavior component with the Bullet object
        bulletInstance.GetComponent<BulletBehavior>().Initialize(bullet);
    }

    // Setters for encapsulation
    void setIsLaser(bool isLaser)
    {
        this.isLaser = isLaser;
    }

    void setIsOnFire(bool isOnFire)
    {
        this.isOnFire = isOnFire;
    }

    void setShouldSplit(int shouldSplit)
    {
        this.shouldSplit = shouldSplit;
    }
}
