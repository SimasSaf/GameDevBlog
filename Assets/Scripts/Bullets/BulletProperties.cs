public class BulletProperties
{
    public int damage { get; }
    public float bulletSpeed { get; }
    public int fireLevel { get; }
    public int splitLevel { get; }

    public BulletProperties(int damage, float bulletSpeed, int fireLevel, int splitLevel)
    {
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.fireLevel = fireLevel;
        this.splitLevel = splitLevel;
    }
}
