# First Blog Entry
In this post the process made so far will be covered an the reasoning behind decisions.

## Hierarchy
So far the chierarchy looks like this:

![Hierarchy](/Blog/resources/entry1-hierarchy.png)

## UI
The UI is very simple for now with not much function. There are three buttons for Single-Player, Multi-Player and Exit. Only Single-Player works for now. 
In the menu the game itself can be seen, this is a stylistic choice. And when Single-Player is chose, the camera move to center the player object (earth) then the enemies start spawning.

![UI](/Blog/resources/entry1-UI.png)

Future for UI:
- Add pause button bringing up a different version of UI
- Add "Options"
- Make Exit work
- Make Multi-Player work

## Gameplay
From the gameplay side of things, currently the planet has a cannon that rotates around the earth constantly shooting bullets while enamies spawn around and approach the earth.

![Gameplay](/Blog/resources/entry1-gameplay.png)

Future for Gameplay:
- Add shooting upgrades
- Add Second-Player object

### Earth
Earth is the object hat requires protecting from the enemies of the game. 
The earth spins around its axis constantly. It has both a 2D collider and a rigidbody. Earth has a health system implemented but it is currently disabled to not end the game prematurely. The earth also manages its own collisions with a custom script that destroys enamies (this might change, still a topic for conversation).

### Cannon
The cannon is a player controlled game object that rotates around the earth using keyboard (will implement proper controlls later). Bullets spawn in the cannon and move in the direction where the cannon is facing, but that is done in the bullet object.

### Bullet
So far there are only one type of bullet which has no special effect. The bullet prefab has a 2D collider and a rigidbody and a script called "BulletBehavior". Which is responsible of keeping track what to do with the bullets attributes given from "BulletManager" script. And it manages on collision events.

The "BulletManager" script for the "BulletManager" game object is responsible for bullet attributes like "isOnFire", "isLaser", etc. It also is in control of the bullet speed and fire rate. The script initializes bullets (this will be changed into bullet pools).
For the bullet spawning location a shootingPoint transform is attached (cannon). This script "shoots" bullets.

`public class BulletBehavior : MonoBehaviour
{
    public Bullet bulletProperties; // Holds the specific properties of the bullet

    public void Initialize(Bullet bullet)
    {
        bulletProperties = bullet;
        // Here, you can add code to change the GameObject's behavior based on bulletProperties
        // For example, changing the appearance or enabling certain effects such as isOnFire or isLaser
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Print the collision to the console
        print("Collision with " + collision.gameObject.name);

        // Check if the collided object has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy GameObject
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }   
}`

Bullets destroy enemies.

### Enemies
So far there are 2 different prefabs for enemies. Enamies are spawned using the "EnemySpawnManager" which uses "EnemyPoolManager" to not constantly initialize new object (that tanks performance). Instead the pool manager initializes enemy game objects when the game is launched and then they are only activated by the spawn manager.
Enemies constantly move towards the earth (the transform is attached programmatically) in a straight line, on collision they are destroyed.

Below are the attributes that the "EnemySpawnManager" is in control of:

`public class EnemySpawnManager : MonoBehaviour
{
    public float spawnRate = 2f; // Time between spawns
    public float spawnDistance = 10f; // Distance from the Earth to spawn enemies
    public Transform earthTransform; // Assign the Earth's transform
    public int enemiesPerSpawn = 1; // Number of enemies to spawn each time

    private bool spawnEnemies = false;
    private float nextSpawnTime = 0f; // Initialize nextSpawnTime

    void Update()
    {
        if (spawnEnemies && Time.time >= nextSpawnTime)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                SpawnEnemy();
            }
            nextSpawnTime = Time.time + spawnRate;
        }
    }
    ...Other methods...
}`

It also shows how the Update method handles spawn rate.

### Background
Just a static image of stars...


