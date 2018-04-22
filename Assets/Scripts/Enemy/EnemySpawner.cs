using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Transform spawnTransform;
    public Transform exitTransform;
    public List<GameObject> enemyPrefabs;
    public float spawnInterval;
    public EnemyActionEvent onEnemyDeath;
    public EnemyActionEvent onEnemyExit;
    public DifficultyManager difficultyManager;

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
        _spawnCounter -= Time.deltaTime * difficultyManager.difficulty;

        if (_spawnCounter < 0)
        {
            var spawnWave = Random.Range(0f, 1f) < difficultyManager.difficulty / difficultyManager.maxDifficulty;
            var waveEnemyNumber = 1;

            if (spawnWave)
                waveEnemyNumber = Random.Range(1, (int)(3 * difficultyManager.difficulty));

            for (int i = 0; i < waveEnemyNumber; i++)
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
