using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IGameEntity
{
    [SerializeField] protected Bullet bulletPrefab;
    protected float _moveSpeed = 10;

    protected int bulletInterval => 300;
    protected int bulletCountdown = 0;


    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void TakeDamage(int damage)
    {
        Vector3 newPosition = new Vector3(transform.position.x + 20f, Random.Range(Constants.MinY, Constants.MaxY), 0f);
        transform.position = newPosition;
    }

    protected void Shoot()
    {
        if (bulletCountdown == 0)
        {
            bulletCountdown = bulletInterval;

            Bullet bull = Instantiate<Bullet>(bulletPrefab);
            bull.transform.position = transform.position + Vector3.left;
            bull.Shoot(Vector3.left, 20f);
        }
        if (bulletCountdown > 0)
        {
            bulletCountdown--;
        }
    }
}
