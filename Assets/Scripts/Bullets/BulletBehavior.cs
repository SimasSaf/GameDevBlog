using TMPro;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    public int damage;
    public GameObject damageNumberPrefab;

    [SerializeField]
    private int splitLevel = 0;

    [SerializeField]
    public int fireLevel = 0;
    private BulletProperties bulletProperties;
    private IBulletPoolManger iBulletPoolManager;
    private IBulletSplit iBulletSplit;

    private float boundsOffset = 5f;

    void Awake()
    {
        iBulletSplit = FindObjectOfType<BulletSpawnManager>();
    }

    void Update()
    {
        destroyBulletIfLeftScreen();
    }

    public void Initialize(BulletProperties bulletProperties)
    {
        iBulletPoolManager = FindObjectOfType<BulletPoolManager>();
        this.bulletProperties = bulletProperties;
        damage = bulletProperties.damage;
        splitLevel = bulletProperties.splitLevel;
        fireLevel = bulletProperties.fireLevel;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            if (enemy.health <= damage)
            {
                if (splitLevel > 0)
                {
                    Split();
                }
            }
            Destroy(gameObject);
        }
    }

    void destroyBulletIfLeftScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (
            screenPosition.x < -boundsOffset
            || screenPosition.x > 1 + boundsOffset
            || screenPosition.y < -boundsOffset
            || screenPosition.y > 1 + boundsOffset
        )
        {
            iBulletPoolManager.ReturnBulletToPool(gameObject);
        }
    }

    void Split()
    {
        Vector3 currentPosition = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z
        );

        iBulletSplit.SpawnBulletsAtRandomDirections(currentPosition, splitLevel);
    }
}
