using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class MovePlayerNavMesh : MonoBehaviour
{
    private PlayerActions playerActions;
    private InputAction mousePosition;
    private InputAction mouseClick;
    private NavMeshAgent agent;

    [SerializeField] private LayerMask layerMask;
    private Vector3 destination;

    void Awake()
    {
        playerActions = new PlayerActions();
        mousePosition = playerActions.Movement.Position;
        mouseClick = playerActions.Movement.Click;
    }

    void OnEnable()
    {
        mousePosition.Enable();
        mouseClick.Enable();
    }

    void OnDisable()
    {
        mousePosition.Disable();
        mouseClick.Disable();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        mouseClick.performed += GetDestination;
        destination = transform.position;
    }

    private void GetDestination(InputAction.CallbackContext context)
    {
        Vector2 position = mousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            destination = hit.point;
            agent.SetDestination(destination);
        }
    }
    void OnDrawGizmos()
    {
        if (agent == null || agent.path == null) return;

        Gizmos.color = Color.yellow;

        Vector3[] corners = agent.path.corners;
        for (int i = 0; i < corners.Length - 1; i++)
        {
            Gizmos.DrawLine(corners[i], corners[i + 1]);
        }
    }
}
