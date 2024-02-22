using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] protected ParticleSystem _particleSystem;

    public void Explode()
    {
        _particleSystem.Play();
    }

    void OnParticleSystemStopped()
    {
        // Return to the pool
        PoolingManager.Instance.ReturnObjectToPool(gameObject);
    }
}
