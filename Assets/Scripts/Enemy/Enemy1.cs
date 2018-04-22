using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour {

    public Transform target;
    public EnemyActionEvent onDeath;
    public EnemyActionEvent onExit;
    public int moneyDropped;
    public int life;

    private NavMeshAgent _agent;
    private SpriteRenderer _renderer;
    private int hitCount;

    private Color[] LifeColors = new Color[] {
        Color.red,
        new Color(1, 0.65f, 0), // orange
        Color.yellow,
        Color.green,
        Color.white
    };

    void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        UpdateColor();
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
        {
            life--;
            hitCount++;
            UpdateColor();

            if (life <= 0)
                onDeath.Invoke(new EnemyAction(this));
        }
    }

    private void UpdateColor()
    {
        _renderer.material.color = LifeColors[Mathf.Clamp(hitCount, 0, LifeColors.Length)];
    }
}
