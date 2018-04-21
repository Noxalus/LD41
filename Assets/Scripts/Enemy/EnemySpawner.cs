using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour {

    public Transform spawnTransform;
    public Transform exitTransform;
    public List<GameObject> enemyPrefabs;
    public float spawnInterval;
    public EnemyActionEvent onEnemyDeath;
    public EnemyActionEvent onEnemyExit;

    private float _spawnCounter;

	void Start ()
    {
        _spawnCounter = spawnInterval;

        if (onEnemyDeath == null)
            onEnemyDeath = new EnemyActionEvent();
        if (onEnemyExit == null)
            onEnemyExit = new EnemyActionEvent();
    }

    void Update ()
    {
        _spawnCounter -= Time.deltaTime;

        if (_spawnCounter < 0)
        {
            SpawnEnemy();
            _spawnCounter = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        var newEnemy = Instantiate(enemyPrefabs[0], spawnTransform.position, spawnTransform.rotation);
        var newEnemyScript = newEnemy.GetComponent<Enemy1>();
        newEnemyScript.target = exitTransform;
        newEnemyScript.onDeath.AddListener(EnemyDeath);
        newEnemyScript.onExit.AddListener(EnemyExit);
    }

    private void EnemyDeath(EnemyAction enemyAction)
    {
        onEnemyDeath.Invoke(enemyAction);
        Destroy(enemyAction.enemy.gameObject);
    }

    private void EnemyExit(EnemyAction enemyAction)
    {
        onEnemyExit.Invoke(enemyAction);
        Destroy(enemyAction.enemy.gameObject);
    }
}
