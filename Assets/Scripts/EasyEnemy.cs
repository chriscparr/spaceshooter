using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemy : AbstractEnemy
{
    protected override float bulletInterval => 2f;

    protected override int Health { get => health; set => health = value; }
    protected override int TotalHealth => 1;

    public override Constants.EnemyType Type => Constants.EnemyType.EASY;

    protected override float bulletSpeed => 20f;

    protected int health;

    protected Vector3 playerDirection;

    protected override void Start()
    {
        base.Start();
        Player.PlayerPositionChanged += OnPlayerPositionChanged;
    }

    protected void OnPlayerPositionChanged(Vector3 playerPosition)
    {
        playerDirection = (playerPosition - transform.position).normalized;
    }

    protected override void Shoot()
    {
        Bullet bullet = PoolingManager.Instance.GetObjectFromPool(Constants.BULLET_PREFAB_NAME, true, 0).GetComponent<Bullet>();
        bullet.transform.position = transform.position + Vector3.left;
        bullet.FireBullet(playerDirection, bulletSpeed);
    }
}
