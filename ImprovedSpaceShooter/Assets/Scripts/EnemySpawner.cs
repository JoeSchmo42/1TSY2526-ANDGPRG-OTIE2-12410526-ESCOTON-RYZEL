using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Vector3> spawnPositions;
    private int currentLevel = 1;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        SpawnEnemies();
        Debug.Log($"Level {currentLevel} started");
    }

    void Update()
    {
        // Check if all enemies are defeated
        if (activeEnemies.Count == 0)
        {
            LevelComplete();
        }
    }

    void SpawnEnemies()
    {
        foreach (Vector3 position in spawnPositions)
        {
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            activeEnemies.Add(enemy);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                
                enemyScript.OnEnemyDestroyed += RemoveEnemy;
            }
        }
    }

    void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }

    void LevelComplete()
    {
        AudioManager.Instance.PlayLevelCompleteSFX();
        currentLevel++;
        activeEnemies.Clear();
        SpawnEnemies();
        Debug.Log($"Level {currentLevel} started");
    }
}