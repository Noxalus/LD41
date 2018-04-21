using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour {

    public Transform target;
    public EnemyActionEvent onDeath;
    public EnemyActionEvent onExit;
    public int moneyDropped;

    private NavMeshAgent _agent;

	void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

	void Update ()
    {
        _agent.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        onDeath.Invoke(new EnemyAction(this));
    }

    private void OnTriggerEnter(Collider other)
    {
        onExit.Invoke(new EnemyAction(this));
    }
}
