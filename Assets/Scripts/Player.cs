using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractGameEntity
{ 
    [SerializeField] protected Bullet bulletPrefab;
    public override Bullet BulletPrefab { get { return bulletPrefab; } }

    protected override int Health { get => _health; set => _health = value; }

    protected override int TotalHealth => 100;
    protected override int bulletInterval => 100;
    protected int _health;
    protected float _moveSpeed = 10;

    // Update is called once per frame
    protected override void Update()
    {
        Shoot();

        float horizontalInput = Input.GetAxis("Horizontal");
        //Get the value of the Horizontal input axis.

        float verticalInput = Input.GetAxis("Vertical");
        //Get the value of the Vertical input axis.

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _moveSpeed * Time.deltaTime);
    }

    protected override void Shoot()
    {
        if (bulletCountdown == 0)
        {
            bulletCountdown = bulletInterval;

            Bullet bull = Instantiate<Bullet>(bulletPrefab);
            bull.transform.position = transform.position + Vector3.right;
            bull.FireBullet(Vector3.right, 20f);
        }
        if (bulletCountdown > 0)
        {
            bulletCountdown--;
        }
    }

    public override void TakeDamage(int damage)
    {
        //take some damage
    }
}
