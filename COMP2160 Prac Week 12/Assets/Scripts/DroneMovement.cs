using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    private NavMeshAgent agent;

    [SerializeField] private LayerMask layerMask;
    private Vector3 destination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    void OnDrawGizmos()
    {
        if (agent == null || agent.path == null || player == null) return;

        Gizmos.color = Color.red;

        Vector3[] corners = agent.path.corners;
        for (int i = 0; i < corners.Length - 1; i++)
        {
            Gizmos.DrawLine(corners[i], corners[i + 1]);
        }
    }
}
