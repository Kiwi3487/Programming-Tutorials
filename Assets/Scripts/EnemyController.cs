using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float wanderRange; // radius of the wander area
    public float chaseRange; // range for chasing the player
    public Transform centrePoint; // centre of the wander area
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming player tag is "Player"
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            // Player is within chase range, chase the player
            agent.SetDestination(player.position);
        }
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Done with path, wander randomly
            Vector3 wanderPoint;
            if (RandomPoint(centrePoint.position, wanderRange, out wanderPoint))
            {
                Debug.DrawRay(wanderPoint, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(wanderPoint);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}