using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGameEntity : MonoBehaviour, IGameEntity
{
    protected abstract int Health { get; set; }
    protected abstract int TotalHealth { get; }
    protected abstract float bulletInterval { get; }

    public abstract void TakeDamage(int damage);

    protected virtual void Start()
    {
        Health = TotalHealth;
    }

    protected virtual IEnumerator ShootInterval()
    {
        while(true)
        {
            yield return new WaitForSeconds(bulletInterval);
            if (Health <= 0)
            {
                yield break;
            }
            Shoot();
        }        
    }

    protected abstract void Shoot();
}
