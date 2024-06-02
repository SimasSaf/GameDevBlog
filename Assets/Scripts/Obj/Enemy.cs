public class Enemy
{
    public int health { get; }
    public float speed { get; }
    public int fireLevel { get; set; }

    // Constructor to initialize a new Bullet object with specific values
    public Enemy(int health, float speed)
    {
        this.health = health;
        this.speed = speed;
    }
}
