using UnityEngine;

public abstract class AbstractGameEntity : MonoBehaviour, IGameEntity
{
    protected abstract int Health { get; set; }
    protected abstract int TotalHealth { get; }
    protected abstract int bulletInterval { get; }
    protected int bulletCountdown = 0;

    protected BulletFactory _bulletFactory;

    public abstract void TakeDamage(int damage);

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
