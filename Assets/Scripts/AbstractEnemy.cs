using UnityEngine;
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
            EnemyKilled?.Invoke(this);
        }
    }

    public void Spawn(Vector3 spawnPosition)
    {
        Health = TotalHealth;
        transform.position = spawnPosition;
    }
}
