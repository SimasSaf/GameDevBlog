using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Enemy enemy;

    [SerializeField]
    public int health;
    private ILeveling leveling;

    [SerializeField]
    private int experienceGiven = 10;

    void Awake()
    {
        leveling = FindAnyObjectByType<LevelingSystem>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            BulletBehavior bullet = collision.gameObject.GetComponent<BulletBehavior>();
            if (bullet != null)
            {
                Debug.LogError("Enemy collided Bullet");
                TakeDamage(bullet.damage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Earth"))
        {
            Debug.Log("Enemy triggered with Earth");
            Die();
        }
    }

    public void Initialize(Enemy enemy)
    {
        health = enemy.health;
    }

    private void Die()
    {
        EnemyPoolManager.Instance.ReturnEnemyToPool(gameObject);
        leveling.AddExperience(experienceGiven);
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
}
