using UnityEngine;
using System.Collections;
public abstract class AbstractEnemy : AbstractGameEntity, IEnemy
{
    public abstract Constants.EnemyType Type { get; }
    public Vector3 Position => transform.position;

    public event IEnemy.EnemyStatus EnemyKilled;

    public override void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            StopCoroutine(ShootInterval());
            EnemyKilled?.Invoke(this);
        }
    }

    public void Spawn(Vector3 spawnPosition)
    {
        StartCoroutine(ShootInterval());
        Health = TotalHealth;
        transform.position = spawnPosition;
    }
}
