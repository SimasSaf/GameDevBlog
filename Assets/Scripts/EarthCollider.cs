using UnityEngine;

public class EarthCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Print the collision to the console
        print("Collision with " + collision.gameObject.name);

        // Check if the collided object has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy GameObject
            Destroy(collision.gameObject);
        }
    }   
}
