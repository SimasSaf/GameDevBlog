using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 20f;
    public float fireRate = 1f; // Bullets per second

    private float nextFireTime = 0f; // When the next shot is due

    void Update()
    {
        // Check if it's time to shoot again
        if (Time.time >= nextFireTime)
        {
            ShootBullet();
            // Set the next fire time by adding the inverse of the fire rate to the current time
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            Vector2 forceDirection = -shootPoint.up.normalized * bulletSpeed;
            bulletRb.AddForce(forceDirection, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component not found on the bullet prefab.");
        }
    }
}
