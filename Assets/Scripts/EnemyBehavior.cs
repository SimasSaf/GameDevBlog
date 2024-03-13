using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Enemy enemy; // Holds the specific properties of the enemy

    void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    public void Initialize(Enemy enemy)
    {
        this.enemy = enemy;
    }

    private void Die()
    {
        EnemyPoolManager.Instance.ReturnEnemyToPool(this.gameObject);
    }
}