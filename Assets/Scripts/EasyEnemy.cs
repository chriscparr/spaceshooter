using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemy : AbstractEnemy
{
    protected override int bulletInterval => 500;

    protected override int Health { get => health; set => health = value; }
    protected override int TotalHealth => 1;
    protected int health;    

    protected override void Shoot()
    {
        if (bulletCountdown == 0)
        {
            bulletCountdown = bulletInterval;
            Vector3 direction = (GameController.Instance.PlayerPosition - transform.position).normalized;
            IBullet bull = _bulletFactory.GetBullet(transform.position + direction * 2f);
            bull.FireBullet(direction, 20f);
        }
        if (bulletCountdown > 0)
        {
            bulletCountdown--;
        }
    }
}
