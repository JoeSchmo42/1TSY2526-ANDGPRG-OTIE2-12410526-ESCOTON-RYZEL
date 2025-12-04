using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    [Header("Monster Prefabs")]
    public GameObject groundMonster;
    public GameObject flyingMonster;
    public GameObject bossMonster;

    [Header("Wave Settings")]
    public Transform spawnPoint;
    public int waveNumber = 1;
    public float waveInterval = 12f;

    private bool waveActive = false;

    void Start() => StartCoroutine(SpawnWaves());

    IEnumerator SpawnWaves()
    {
        while (true)
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
        int monstersToSpawn = waveNum * 4;

        for (int i = 0; i < monstersToSpawn; i++)
        {
            GameObject prefab = groundMonster;
            float rnd = Random.value;
            if (rnd < 0.3f) prefab = flyingMonster;
            else if (rnd < 0.05f) prefab = bossMonster;

            GameObject monster = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

            // Scale difficulty
            MonsterHealth mh = monster.GetComponent<MonsterHealth>();
            if (mh != null)
                mh.maxHealth *= (1f + waveNum * 0.4f);

            NavMeshAgent agent = monster.GetComponent<NavMeshAgent>();
            if (agent != null)
                agent.speed *= (1f + waveNum * 0.15f);

            yield return new WaitForSeconds(0.9f);
        }

        yield return StartCoroutine(WaitForWaveClear());
        waveActive = false;
    }

    IEnumerator WaitForWaveClear()
    {
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            yield return new WaitForSeconds(1f);
    }
}
