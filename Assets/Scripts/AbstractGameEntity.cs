using UnityEngine;

public abstract class AbstractGameEntity : MonoBehaviour, IGameEntity
{
    protected abstract int Health { get; set; }
    protected abstract int TotalHealth { get; }
    protected abstract int bulletInterval { get; }
    protected int bulletCountdown = 0;

    protected BulletFactory _bulletFactory;

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
        _bulletFactory = gameObject.AddComponent<BulletFactory>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Shoot();
    }

    protected abstract void Shoot();
}
