using UnityEngine;

public abstract class AbstractGameEntity : MonoBehaviour, IGameEntity
{
    public abstract Bullet BulletPrefab { get; }

    protected abstract int Health { get; set; }
    protected abstract int TotalHealth { get; }
    protected abstract int bulletInterval { get; }
    protected int bulletCountdown = 0;

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Start()
    {
        Health = TotalHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Shoot();
    }

    protected abstract void Shoot();
}
