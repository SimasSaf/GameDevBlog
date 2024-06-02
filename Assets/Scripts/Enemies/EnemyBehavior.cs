using UnityEngine;
using TMPro;

public class EnemyBehavior : MonoBehaviour
{
    public Enemy enemy;
    public GameObject damageNumberPrefab;
    public GameObject explosionPrefab;

    [SerializeField]
    public int health;
    private ILeveling leveling;

    [SerializeField]
    private int experienceGiven = 10;

    [SerializeField]
    private float explosionRadius = 10f;

    [SerializeField]
    private int explosionDamage = 10;

    [SerializeField]
    private float fireDamageInterval = 1f; // How often to take dmg

    [SerializeField]
    private int scaleFireDamage = 2; // Fire damage is fire level * this number

    private bool isOnFire = false;
    private float fireDamageTimer;

    void Awake()
    {
        leveling = FindAnyObjectByType<LevelingSystem>();
    }

    void OnEnable()
    {
        // Resetting state for when we again reuse this
        isOnFire = false;
        fireDamageTimer = fireDamageInterval;
    }

    void Update()
    {
        if (isOnFire)
        {
            fireDamageTimer -= Time.deltaTime;
            if (fireDamageTimer <= 0f)
            {
                TakeDamage(enemy.fireLevel * scaleFireDamage);
                fireDamageTimer = fireDamageInterval;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            BulletBehavior bullet = collision.gameObject.GetComponent<BulletBehavior>();
            if (bullet != null)
            {
                if (bullet.fireLevel > 0)
                {
                    SetOnFire(bullet.fireLevel);
                }
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
        this.enemy = enemy;
        health = enemy.health;
    }

    private void Die()
    {
        if (enemy.fireLevel >= 3)
        {
            Explode();
        }
        isOnFire = false;
        EnemyPoolManager.Instance.ReturnEnemyToPool(gameObject);
        leveling.AddExperience(experienceGiven);
    }

    public void TakeDamage(int damage)
    {
        ShowDamageNumber(damage);

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(
            explosionPrefab,
            transform.position,
            Quaternion.identity
        );

        Explosion explosionScript = explosion.GetComponent<Explosion>();
        if (explosionScript != null)
        {
            explosionDamage = explosionDamage * enemy.fireLevel - 40; // this is done so that it starts off as 10, a little hack (potential for bugs :D)

            explosionScript.Initialize(explosionDamage, enemy.fireLevel, explosionRadius);
        }
    }

    public void SetOnFire(int fireLevel)
    {
        if (fireLevel <= 0 || isOnFire)
            return;

        enemy.fireLevel = fireLevel;
        isOnFire = true;
        fireDamageTimer = fireDamageInterval;
    }

    void ShowDamageNumber(int damage)
    {
        GameObject dmgNumber = Instantiate(
            damageNumberPrefab,
            gameObject.transform.position + new Vector3(9.5f, -2f, 0),
            Quaternion.identity
        );

        dmgNumber.GetComponent<TextMeshPro>().text = damage.ToString();

        Destroy(dmgNumber, 2f);
    }
}
