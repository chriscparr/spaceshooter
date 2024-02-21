using UnityEngine;
public class HardEnemy : AbstractEnemy
{
    [SerializeField] protected Bullet bulletPrefab;
    public override Bullet BulletPrefab { get { return bulletPrefab; } }

    protected override int Health { get => _health; set => _health = value; }

    protected override int TotalHealth => 2;
    protected override int bulletInterval => 700;
    protected int _health;

    private Vector3 _bulletYOffset = new Vector3(0f, 0.1f, 0f);
    private Vector3 bulletSpawnPoint => transform.position + new Vector3(-1.5f, 0f, 0f);

    protected override void Shoot()
    {
        if (bulletCountdown == 0)
        {
            bulletCountdown = bulletInterval;

            Bullet bull1 = Instantiate<Bullet>(BulletPrefab);
            bull1.transform.position = bulletSpawnPoint + Vector3.up;

            Bullet bull2 = Instantiate<Bullet>(BulletPrefab);
            bull2.transform.position = bulletSpawnPoint;

            Bullet bull3 = Instantiate<Bullet>(BulletPrefab);
            bull3.transform.position = bulletSpawnPoint + Vector3.down;

            bull1.FireBullet(Vector3.left + _bulletYOffset, 20f);
            bull2.FireBullet(Vector3.left, 20f);
            bull3.FireBullet(Vector3.left - _bulletYOffset, 20f);
        }
        if (bulletCountdown > 0)
        {
            bulletCountdown--;
        }
    }
}