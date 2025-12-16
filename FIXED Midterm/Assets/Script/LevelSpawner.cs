using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject cheesePrefab;
    public GameObject enemyPrefab;
    public int cheeseToSpawn = 5;
    public Vector3[] cheeseSpawnPoints;
    public Vector3 enemySpawnPosition = new Vector3(4.78f, 3.67f, 18.91f);

    void Start()
    {
        if (cheeseSpawnPoints.Length < cheeseToSpawn)
        {
            Debug.LogError($"Not enough spawn points! Expected: {cheeseToSpawn}, Got: {cheeseSpawnPoints.Length}");
            return;
        }

        GameManager.Instance.cheeseCount = 0;
        for (int i = 0; i < cheeseToSpawn; i++)
        {
            GameObject cheese = Instantiate(cheesePrefab, cheeseSpawnPoints[i], Quaternion.identity);
            Debug.Log($"Spawned cheese at: {cheeseSpawnPoints[i]}");
        }

        GameObject enemy = Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
        Debug.Log($"Enemy spawned at: {enemySpawnPosition}");

        Debug.Log($"Total cheese spawned: {cheeseToSpawn}");
    }
}
