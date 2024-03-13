using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public Bullet bulletProperties; // Holds the specific properties of the bullet

    public void Initialize(Bullet bullet)
    {
        bulletProperties = bullet;
        // Here, you can add code to change the GameObject's behavior based on bulletProperties
        // For example, changing the appearance or enabling certain effects
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Print the collision to the console
        print("Collision with " + collision.gameObject.name);

        // Check if the collided object has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy GameObject
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }   
}
