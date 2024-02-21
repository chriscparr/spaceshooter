using UnityEngine;

public interface IBullet
{
    public void FireBullet(Vector3 direction, float speed);
}

public abstract class Factory : MonoBehaviour
{
    public abstract IBullet GetBullet(Vector3 position);
}
