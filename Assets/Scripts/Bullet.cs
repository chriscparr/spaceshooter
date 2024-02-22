using UnityEngine;

public class Bullet : MonoBehaviour, IGameEntity, IBullet
{
    [SerializeField] protected Rigidbody _rigidbody;

    public void FireBullet(Vector3 direction, float speed)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<IGameEntity>()?.TakeDamage(1);
        TakeDamage(1);
    }

    //Trigger box attached to the camera will move with the player + dispose of stray bullets
    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Main Camera")
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        PoolingManager.Instance.ReturnObjectToPool(gameObject);
    }
}
