using UnityEngine;

public class BulletSpawnManager
    : MonoBehaviour,
        IBulletSpawnManager,
        IBulletSplit,
        ILevelingSystemObserver
{
    private IBulletPoolManger iBulletPoolManager;

    [SerializeField]
    private Transform shootingPoint;
    private ISoundEffectManager iSoundEffectManager;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float fireRate;

    [SerializeField]
    private int splitLevel;

    [SerializeField]
    private int fireLevel;

    [SerializeField]
    private float nextFireTime;

    private LevelingSystem levelingSystem;

    private void Awake()
    {
        damage = 10;
        bulletSpeed = 5f;
        fireRate = 0.5f;
        splitLevel = 0;
        fireLevel = 0;

        iSoundEffectManager = FindAnyObjectByType<AudioManager>();
        iBulletPoolManager = FindAnyObjectByType<BulletPoolManager>();
        levelingSystem = FindAnyObjectByType<LevelingSystem>();

        if (levelingSystem != null)
        {
            levelingSystem.RegisterObserver(this);
        }
    }

    private void OnDestroy()
    {
        if (levelingSystem != null)
        {
            levelingSystem.UnregisterObserver(this);
        }
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    public void IncreaseBulletProperties(
        int? damage,
        float? fireRate,
        int? splitLevel,
        int? fireLevel
    )
    {
        if (damage.HasValue)
        {
            this.damage += 5;
        }

        if (fireRate.HasValue)
        {
            this.fireRate += fireRate.Value;
        }

        if (splitLevel.HasValue)
        {
            this.splitLevel += splitLevel.Value;
        }

        if (fireLevel.HasValue)
        {
            this.fireLevel += fireLevel.Value;
        }
    }

    public void SpawnBulletsAtRandomDirections(Vector3 spawnPosition, int numberOfBullets)
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject bulletInstance = iBulletPoolManager.GetPooledBullet();
            bulletInstance.transform.position = spawnPosition;

            float angle = Random.Range(0f, 360f);
            bulletInstance.transform.rotation = Quaternion.Euler(0, 0, angle);

            Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = bulletInstance.transform.up * bulletSpeed;
            bulletRb.AddForce(forceDirection, ForceMode2D.Impulse);

            SetBulletPropertiesForBulletInstance(bulletInstance);
        }
    }

    void ShootBullet()
    {
        GameObject bulletInstance = iBulletPoolManager.GetPooledBullet();
        Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();

        SetBulletFlyingDirectionAndSpeed(bulletInstance, bulletRb);

        iSoundEffectManager.ShootSoundEffect();

        SetBulletPropertiesForBulletInstance(bulletInstance);
    }

    private void SetBulletFlyingDirectionAndSpeed(GameObject bulletInstance, Rigidbody2D bulletRb)
    {
        bulletInstance.transform.position = shootingPoint.position;
        bulletInstance.transform.rotation = shootingPoint.rotation;

        Vector2 forceDirection = -shootingPoint.up.normalized * bulletSpeed;
        bulletRb.AddForce(forceDirection, ForceMode2D.Impulse);
    }

    private void SetBulletPropertiesForBulletInstance(GameObject bulletInstance)
    {
        BulletBehavior bulletBehavior = bulletInstance.GetComponent<BulletBehavior>();

        BulletProperties bullet = new BulletProperties(damage, bulletSpeed, fireLevel, splitLevel);

        if (bulletBehavior != null)
        {
            bulletBehavior.Initialize(bullet);
        }
    }

    public void OnLevelUp(int newLevel) { }

    public void OnReset()
    {
        damage = 10;
        bulletSpeed = 5f;
        fireRate = 0.5f;
        splitLevel = 0;
        fireLevel = 0;
    }

    public void OnAddExperience(int experience, int experienceToNextLevel) { }
}
