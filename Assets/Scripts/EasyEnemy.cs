using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemy : AbstractEnemy
{
    [SerializeField] protected Bullet bulletPrefab;
    public override Bullet BulletPrefab { get { return bulletPrefab; } }
    protected override int bulletInterval => 500;

    protected override int Health { get => health; set => health = value; }
    protected override int TotalHealth => 1;
    protected int health;
    
    

    protected override void Shoot()
    {
        if (bulletCountdown == 0)
        {
            bulletCountdown = bulletInterval;

            Bullet bull = Instantiate<Bullet>(BulletPrefab);
            Vector3 direction = (GameController.Instance.PlayerPosition - transform.position).normalized;

            bull.transform.position = transform.position + direction * 2f;
            bull.FireBullet(direction, 20f);
        }
        if (bulletCountdown > 0)
        {
            bulletCountdown--;
        }
    }
}
