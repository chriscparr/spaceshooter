using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public delegate void SpawnStatus(List<Vector3> spawnPositions);
    public static event SpawnStatus SpawnPositionsChanged;

    [SerializeField] protected AbstractEnemy[] _enemies;

    // Start is called before the first frame update
    void Start()
    {
        foreach(IEnemy e in _enemies)
        {
            e.EnemyKilled += OnEnemyKilled;
        }
        DispatchSpawnPositions();
    }

    private void OnEnemyKilled(IEnemy enemy)
    {
        Explosion explosion = PoolingManager.Instance.GetObjectFromPool("Explosion", true, 0).GetComponent<Explosion>();
        explosion.transform.position = enemy.Position;
        explosion.Explode();

        enemy.Spawn(new Vector3(enemy.Position.x + Random.Range(10f, 20f), Random.Range(Constants.MinY, Constants.MaxY), 0f));
        DispatchSpawnPositions();
    }

    protected void DispatchSpawnPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (IEnemy e in _enemies)
        {
            positions.Add(e.Position);
        }
        SpawnPositionsChanged?.Invoke(positions);
    }
}
