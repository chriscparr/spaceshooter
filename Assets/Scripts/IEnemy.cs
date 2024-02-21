using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public delegate void EnemyStatus(IEnemy enemy);
    public event EnemyStatus EnemyKilled;
    public Constants.EnemyType Type { get; }
    public Vector3 Position { get; }
    public void Spawn(Vector3 spawnPosition);
}
