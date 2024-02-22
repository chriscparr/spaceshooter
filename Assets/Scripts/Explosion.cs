using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] protected ParticleSystem particleSystem;

    public void Explode()
    {
        particleSystem.Play();
    }

    void OnParticleSystemStopped()
    {
        // Return to the pool
        PoolingManager.Instance.ReturnObjectToPool(gameObject);
    }
}
