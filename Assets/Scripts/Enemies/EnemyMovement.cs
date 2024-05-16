using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float speed;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
