using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : Singleton<EnemyTracker>
{
    public List<GameObject> activeEnemies = new List<GameObject>();

    public int enemyCount => activeEnemies.Count;

    public void AddEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}