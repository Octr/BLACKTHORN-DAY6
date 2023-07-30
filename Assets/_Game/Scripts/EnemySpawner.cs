using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int maxSpawnLimit = 10;

    private void Start()
    {
        // Spawn enemies continuously
        InvokeRepeating("SpawnEnemy", 1f, 2f);
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned to the spawner!");
            return;
        }

        if (Singleton<EnemyTracker>.Instance.enemyCount >= maxSpawnLimit)
        {
            return;
        }

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        Singleton<EnemyTracker>.Instance.AddEnemy(enemy);
    }
}
