// MoveToClickPoint.cs
using UnityEngine;
using UnityEngine.AI;

public class MoveToPoint : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }
}