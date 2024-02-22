using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public delegate void SpawnStatus(List<Vector3> spawnPositions);
    public static event SpawnStatus SpawnPositionsChanged;

    private List<AbstractEnemy> enemyList = new List<AbstractEnemy>();

    private void Awake()
    {
        Player.PlayerSpawned += OnPlayerSpawned;
        Player.PlayerKilled += OnPlayerKilled;
    }

    private void SetupEnemyField()
    {
        for(int i = 0; i < 2; i++)
        {
            HardEnemy hardEnemy = PoolingManager.Instance.GetObjectFromPool("HardEnemy", true, 0).GetComponent<HardEnemy>();
            enemyList.Add(hardEnemy);
            hardEnemy.Spawn(new Vector3(Random.Range(20f, 40f), Random.Range(Constants.MinY, Constants.MaxY), 0f));
        }
        for (int i = 0; i < 3; i++)
        {
            EasyEnemy easyEnemy = PoolingManager.Instance.GetObjectFromPool("EasyEnemy", true, 0).GetComponent<EasyEnemy>();
            enemyList.Add(easyEnemy);
            easyEnemy.Spawn(new Vector3(Random.Range(20f, 40f), Random.Range(Constants.MinY, Constants.MaxY), 0f));
        }
        foreach (IEnemy e in enemyList)
        {
            e.EnemyKilled += OnEnemyKilled;
        }
        DispatchSpawnPositions();
    }

    private void OnPlayerKilled(Vector3 playerPosition)
    {
        Explosion explosion = PoolingManager.Instance.GetObjectFromPool("Explosion", true, 0).GetComponent<Explosion>();
        explosion.transform.position = playerPosition;
        explosion.Explode();

        foreach (AbstractEnemy e in enemyList)
        {
            e.EnemyKilled -= OnEnemyKilled;
            PoolingManager.Instance.ReturnObjectToPool(e.gameObject);
        }
        enemyList.Clear();
    }

    private void OnPlayerSpawned(Vector3 playerPosition)
    {        
        SetupEnemyField();
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
        foreach (IEnemy e in enemyList)
        {
            positions.Add(e.Position);
        }
        SpawnPositionsChanged?.Invoke(positions);
    }
}
