using UnityEngine;
using System.Collections;
public class HardEnemy : AbstractEnemy
{
    protected override int Health { get => _health; set => _health = value; }

    protected override int TotalHealth => 2;
    protected override float bulletInterval => 3f;
    protected int _health;

    public override Constants.EnemyType Type => Constants.EnemyType.HARD;

    private Vector3 _bulletYOffset = new Vector3(0f, 0.1f, 0f);
    private Vector3 bulletSpawnPoint => transform.position + new Vector3(-1.5f, 0f, 0f);

    protected override float bulletSpeed => 20f;

    protected override void Shoot()
    {
        Bullet bull1 = PoolingManager.Instance.GetObjectFromPool(Constants.BULLET_PREFAB_NAME, true, 0).GetComponent<Bullet>();
        bull1.transform.position = bulletSpawnPoint + Vector3.up;
        Bullet bull2 = PoolingManager.Instance.GetObjectFromPool(Constants.BULLET_PREFAB_NAME, true, 0).GetComponent<Bullet>();
        bull2.transform.position = bulletSpawnPoint;
        Bullet bull3 = PoolingManager.Instance.GetObjectFromPool(Constants.BULLET_PREFAB_NAME, true, 0).GetComponent<Bullet>();
        bull3.transform.position = bulletSpawnPoint + Vector3.down;

        bull1.FireBullet(Vector3.left + _bulletYOffset, bulletSpeed);
        bull2.FireBullet(Vector3.left, bulletSpeed);
        bull3.FireBullet(Vector3.left - _bulletYOffset, bulletSpeed);
    }
}