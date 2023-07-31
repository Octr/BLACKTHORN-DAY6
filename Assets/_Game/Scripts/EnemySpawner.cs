using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int maxSpawnLimit = 10;

    private void Start()
    {
        // Spawn enemies continuously
        InvokeRepeating("SpawnEnemy", 1f, 2f);
        EnemyTracker.Instance.maxEnemyCounter = 0; // Reset Spawners
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned to the spawner!");
            return;
        }

        if (EnemyTracker.Instance.enemyCount >= maxSpawnLimit) return;

        if (EnemyTracker.Instance.maxEnemyCounter >= maxSpawnLimit) return;

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        EnemyTracker.Instance.maxEnemyCounter++;
        Singleton<EnemyTracker>.Instance.AddEnemy(enemy);
    }
}
