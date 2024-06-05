using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int explosionDamage;
    public int fireLevel;
    public float explosionRadius;
    public float duration = 0.1f; // Duration before the explosion disappears

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0.2f;
            spriteRenderer.color = color;
        }

        float diameter = explosionRadius * 2;
        transform.localScale = new Vector3(diameter, diameter, 1);

        Destroy(gameObject, duration);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                EnemyBehavior enemyBehavior = hitCollider.GetComponent<EnemyBehavior>();
                if (enemyBehavior != null)
                {
                    enemyBehavior.TakeDamage(explosionDamage);
                    if (fireLevel > 10)
                    {
                        enemyBehavior.SetOnFire(fireLevel);
                    }
                }
            }
        }
    }

    public void Initialize(int damage, int fireLevel, float radius)
    {
        this.explosionDamage = damage;
        this.fireLevel = fireLevel;
        this.explosionRadius = radius;
    }
}
