using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    NavMeshAgent agent;
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(GameManager.Instance.Core.transform.position);
    }
}
