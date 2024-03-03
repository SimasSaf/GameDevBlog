using UnityEngine;

public class EarthCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        
        // Check if the collided object is an enemy
        if (other.CompareTag("Enemy"))
        {
            // Destroy the enemy GameObject
            Destroy(other.gameObject);
        }
    }
}
