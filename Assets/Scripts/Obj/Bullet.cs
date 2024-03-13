public class Bullet
{
    public bool isOnFire { get;}
    public bool isLaser {get;} 
    public int shoudSplit { get;}

    // Constructor to initialize a new Bullet object with specific values
    public Bullet(bool isOnFire, bool isLaser, int shoudSplit)
    {
        this.isOnFire = isOnFire;
        this.shoudSplit = shoudSplit;
        this.isLaser = isLaser;
    }
}