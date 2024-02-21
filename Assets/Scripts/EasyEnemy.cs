using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemy : AbstractEnemy
{
    protected override int bulletInterval => 500;

    protected override int Health { get => health; set => health = value; }
    protected override int TotalHealth => 1;
    protected int health;

    protected Vector3 playerDirection;

    protected override void Start()
    {
        base.Start();
        GameController.Instance.PlayerPositionChanged += OnPlayerPositionChanged;
    }

    protected void OnPlayerPositionChanged(Vector3 playerPosition)
    {
        playerDirection = (playerPosition - transform.position).normalized;
    }


    protected override void Shoot()
    {
        if (bulletCountdown == 0)
        {
            bulletCountdown = bulletInterval;
            IBullet bull = _bulletFactory.GetBullet(transform.position + playerDirection * 2f);
            bull.FireBullet(playerDirection, 20f);
        }
        if (bulletCountdown > 0)
        {
            bulletCountdown--;
        }
    }
}
