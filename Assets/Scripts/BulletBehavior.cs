using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public Bullet bulletProperties;

    private float boundsOffset = 5f; // How far a bullet can go outside the camera view before being deactivated

    public void Initialize(Bullet bullet)
    {
        bulletProperties = bullet;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }   

    void destroyBulletIfLeftScreen(){
        Vector2 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPosition.x < -boundsOffset || screenPosition.x > 1 + boundsOffset ||
            screenPosition.y < -boundsOffset || screenPosition.y > 1 + boundsOffset)
        {
            BulletSpawnManager.Instance.ReturnBulletToPool(gameObject);
        }
    }
}
