using UnityEngine;
using TMPro;
using System.Collections;

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

    private int explosionDamage = 5;

    [SerializeField]
    private float fireDamageInterval = 1f;

    [SerializeField]
    private int scaleFireDamage = 2; // Fire damage is fire level * this number

    private bool isOnFire = false;
    private float fireDamageTimer;

    private Animator anim;
    private Collider2D collider;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        leveling = FindAnyObjectByType<LevelingSystem>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        Transform mainTransform = transform.Find("Main");
        if (mainTransform != null)
        {
            spriteRenderer = mainTransform.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogWarning("'Main' object not found as a child of this prefab.");
        }
    }

    void OnEnable()
    {
        isOnFire = false;
        fireDamageTimer = fireDamageInterval;
        ResetState();
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
        anim.ResetTrigger("FlyTrigger");
        if (collider != null)
        {
            collider.enabled = false;
        }
        else
        {
            Debug.LogWarning("Collider component not found!");
        }

        if (anim != null)
        {
            anim.SetTrigger("DieTrigger");
            StartCoroutine(WaitForStaticPeriod());
        }
        else
        {
            Debug.LogError("Animator component not found!");
        }

        if (enemy.fireLevel >= 3)
        {
            Explode();
        }

        isOnFire = false;
        leveling.AddExperience(experienceGiven);
        visuallyTurnNormal();
    }

    private IEnumerator WaitForStaticPeriod()
    {
        yield return new WaitForSeconds(1.5f);

        if (collider != null)
        {
            collider.enabled = true;
        }

        EnemyPoolManager.Instance.ReturnEnemyToPool(gameObject);
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
            explosionDamage = 5 * enemy.fireLevel - 10;

            explosionScript.Initialize(explosionDamage, enemy.fireLevel, explosionRadius);
        }
    }

    public void SetOnFire(int fireLevel)
    {
        if (fireLevel <= 0 || isOnFire)
            return;

        visuallyTurnRed();
        enemy.fireLevel = fireLevel;
        isOnFire = true;
        fireDamageTimer = fireDamageInterval;
    }

    void visuallyTurnRed()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found on 'main' object.");
        }
    }

    void visuallyTurnNormal()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found on 'main' object.");
        }
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

    private void ResetState()
    {
        visuallyTurnNormal();
        if (anim != null)
        {
            anim.ResetTrigger("DieTrigger");
            anim.SetTrigger("FlyTrigger");
        }

        if (collider != null)
        {
            collider.enabled = true;
        }
    }
}
