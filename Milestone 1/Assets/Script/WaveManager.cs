using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Monster Prefabs")]
    public GameObject groundMonster;
    public GameObject flyingMonster;
    public GameObject bossMonster;

    [Header("Wave Settings")]
    public Transform spawnPoint;
    public int waveNumber = 1;
    public float waveInterval = 10f;  // Seconds between waves

    private bool waveActive = false;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)  // Infinite waves
        {
            if (!waveActive)
            {
                waveActive = true;
                yield return StartCoroutine(SpawnWave(waveNumber));
                waveNumber++;
            }
            yield return new WaitForSeconds(waveInterval);
        }
    }

    IEnumerator SpawnWave(int waveNum)
    {
        int monstersToSpawn = waveNum * 3;
        int bossesSpawned = 0;

        for (int i = 0; i < monstersToSpawn; i++)
        {
            // Mix: 60% ground, 30% flying, 10% boss (increasing bosses later)
            GameObject monster;
            float rand = Random.Range(0f, 1f);
            if (rand < 0.6f) monster = groundMonster;
            else if (rand < 0.9f) monster = flyingMonster;
            else
            {
                monster = bossMonster;
                bossesSpawned++;
            }

            // Difficulty ramp: Scale health/speed by wave
            MonsterHealth healthScript = monster.GetComponentInChildren<MonsterHealth>();
            if (healthScript != null)
            {
                healthScript.health *= (1 + waveNum * 0.5f);
            }
            UnityEngine.AI.NavMeshAgent agent = monster.GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            if (agent != null) agent.speed *= (1 + waveNum * 0.2f);  // Slightly faster

            Instantiate(monster, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1f);  // Stagger spawns
        }

        // Wait for wave to end (poll every second)
        yield return StartCoroutine(WaitForWaveClear());
        waveActive = false;
    }

    IEnumerator WaitForWaveClear()
    {
        while (true)
        {
            // Check if any monsters left (simple tag check; tag all monsters "Enemy")
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0) yield break;  // All gone!

            // Or if they reach end (handled in MonsterMovement Destroy)
            yield return new WaitForSeconds(1f);
        }
    }
}
