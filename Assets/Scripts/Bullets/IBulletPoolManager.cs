using UnityEngine;

public interface IBulletPoolManger
{
    GameObject GetPooledBullet();
    void ReturnBulletToPool(GameObject bullet);
}