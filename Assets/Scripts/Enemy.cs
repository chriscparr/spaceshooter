using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AbstractGameEntity
{
    [SerializeField] protected Bullet bulletPrefab;
    public override Bullet BulletPrefab { get { return bulletPrefab; } }

    protected override int Health { get => _health; set => _health = value; }

    protected override int TotalHealth => 1;
    protected override int bulletInterval => 300;
    protected int _health;
    protected float _moveSpeed = 10;


    public override void TakeDamage(int damage)
    {
        Vector3 newPosition = new Vector3(transform.position.x + 20f, Random.Range(Constants.MinY, Constants.MaxY), 0f);
        transform.position = newPosition;
    }

    protected override void Shoot()
    {
        if (bulletCountdown == 0)
        {
            bulletCountdown = bulletInterval;

            Bullet bull = Instantiate<Bullet>(bulletPrefab);
            bull.transform.position = transform.position + Vector3.left;
            bull.FireBullet(Vector3.left, 20f);
        }
        if (bulletCountdown > 0)
        {
            bulletCountdown--;
        }
    }
}
