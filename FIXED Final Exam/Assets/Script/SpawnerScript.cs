using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnerScript : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] GameObject GoblinPrefab;   
    [SerializeField] GameObject flyingSpiderPrefab;  
    [SerializeField] GameObject bossPrefab;          

    [Header("Spawn Settings")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] float timeBetweenSpawns = 1.5f; 
    [SerializeField] float timeBetweenWaves = 10f;   
    [SerializeField] bool skipFirstWaveDelay = true; 

    [Header("Wave Settings")]
    [SerializeField] int baseEnemyCount = 10;        
    [SerializeField] int enemyIncreasePerWave = 5;   

    [Header("UI")]
    [SerializeField] Text waveText;                  

    private int currentWave = 0;
    private int enemiesToSpawnThisWave = 0;
    private int enemiesSpawnedThisWave = 0;
    private float nextWaveTime;
    public static int EnemiesAlive = 0;              

    void Start()
    {
        StartNextWave();
    }

    void Update()
    {
        // Advance to next wave when all enemies defeated
        if (enemiesSpawnedThisWave == enemiesToSpawnThisWave && EnemiesAlive == 0)
        {
            StartNextWave();
        }

        UpdateWaveUI();
    }

    private void StartNextWave()
    {
        currentWave++;
        enemiesToSpawnThisWave = baseEnemyCount + (currentWave - 1) * enemyIncreasePerWave;
        enemiesSpawnedThisWave = 0;
        EnemiesAlive = 0; // Safety reset

        float delay = (currentWave == 1 && skipFirstWaveDelay) ? 0f : timeBetweenWaves;
        nextWaveTime = Time.time + delay;

        Debug.Log($"=== Preparing Wave {currentWave} with {enemiesToSpawnThisWave} enemies! ===");

        StartCoroutine(DelayedSpawn(delay));
    }

    private IEnumerator DelayedSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnWave());
    }

    private void UpdateWaveUI()
    {
        if (waveText == null) return;

        float timeLeft = nextWaveTime - Time.time;
        if (timeLeft > 0)
        {
            waveText.text = $"Wave {currentWave}\nin {Mathf.CeilToInt(timeLeft)}s";
            return;
        }

        // Wave is active
        if (enemiesSpawnedThisWave < enemiesToSpawnThisWave)
        {
            int remainingToSpawn = enemiesToSpawnThisWave - enemiesSpawnedThisWave;
            waveText.text = $"Wave {currentWave}\nSpawning... ({remainingToSpawn})";
        }
        else if (EnemiesAlive > 0)
        {
            waveText.text = $"Wave {currentWave}\n{EnemiesAlive} left";
        }
        else
        {
            waveText.text = $"Wave {currentWave} Complete!";
        }
    }

    IEnumerator SpawnWave()
    {
        bool isBossWave = (currentWave % 10 == 0);

        for (int i = 0; i < enemiesToSpawnThisWave; i++)
        {
            GameObject prefabToSpawn;

            if (isBossWave && i == enemiesToSpawnThisWave / 2) 
            {
                prefabToSpawn = bossPrefab;
            }
            else
            {
                
                prefabToSpawn = (i % 2 == 0) ? GoblinPrefab : flyingSpiderPrefab;
                
            }

            if (prefabToSpawn != null)
            {
                Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
                EnemiesAlive++;
                enemiesSpawnedThisWave++;
            }

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    
    public static void EnemyDied()
    {
        EnemiesAlive--;
        if (EnemiesAlive < 0) EnemiesAlive = 0;
    }
}