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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Exit")
            onExit.Invoke(new EnemyAction(this));
        else if (other.tag == "Bullet")
            onDeath.Invoke(new EnemyAction(this));
    }
}
