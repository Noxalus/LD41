using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Transform spawnTransform;
    public Transform exitTransform;
    public List<GameObject> enemyPrefabs;
    public float spawnInterval;

    private float _spawnCounter;

	void Start ()
    {
        _spawnCounter = spawnInterval;
    }

    void Update ()
    {
        _spawnCounter -= Time.time;

        if (_spawnCounter < 0)
        {
            SpawnEnemy();
            _spawnCounter = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        var newEnemy = Instantiate(enemyPrefabs[0], spawnTransform.position, spawnTransform.rotation);
        newEnemy.GetComponent<Enemy1>().target = exitTransform;
    }
}
