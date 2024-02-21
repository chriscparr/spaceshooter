using UnityEngine;

public class Bullet : MonoBehaviour, IGameEntity
{
    [SerializeField] protected Rigidbody _rigidbody;

    public void FireBullet(Vector3 direction, float speed)
    {
        _rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<IGameEntity>().TakeDamage(1);
        TakeDamage(1);
    }

    //Trigger box attached to the camera will move with the player + dispose of stray bullets
    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Main Camera")
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
