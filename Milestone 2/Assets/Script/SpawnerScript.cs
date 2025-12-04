using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is
    // 
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawnPoint;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
			Spawn(enemyPrefab); 
		}
    }

    void Spawn(GameObject enemyPref)
    {
        GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        
    }
}
