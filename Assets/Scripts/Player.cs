using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractGameEntity
{
    public delegate void PlayerPositionStatus(Vector3 position);
    public static event PlayerPositionStatus PlayerPositionChanged;

    [SerializeField] protected Rigidbody _rigidbody;
    protected override int Health { get => _health; set => _health = value; }
    protected override int TotalHealth => 100;
    protected override int bulletInterval => 100;
    protected int _health;
    protected float _moveSpeed = 1;    

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(new Vector3(horizontalInput, verticalInput, 0) * _moveSpeed, ForceMode.Force);
        PlayerPositionChanged?.Invoke(transform.position);
    }

    protected override void Shoot()
    {
        if (bulletCountdown == 0)
        {
            bulletCountdown = bulletInterval;
            IBullet bull = _bulletFactory.GetBullet(transform.position + Vector3.right);
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
