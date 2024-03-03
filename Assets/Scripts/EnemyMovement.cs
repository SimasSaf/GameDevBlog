using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // Assign the Earth's transform in the inspector
    public float speed = 5f;

    void Update()
    {
        if (target != null)
        {
            // Move towards the Earth
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
