using UnityEngine;
public abstract class AbstractEnemy : AbstractGameEntity
{
    public override void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Reset();
            Vector3 newPosition = new Vector3(transform.position.x + 20f, Random.Range(Constants.MinY, Constants.MaxY), 0f);
            transform.position = newPosition;
        }
    }

    public virtual void Reset()
    {
        Health = TotalHealth;
    }
}
