using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour {

    public Transform target;

    private NavMeshAgent _agent;

	void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

	void Update ()
    {
        _agent.SetDestination(target.position);
    }
}
