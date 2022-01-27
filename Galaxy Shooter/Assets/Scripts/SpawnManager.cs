using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [Header("Enemy Spawner")]
    [Tooltip("Enemy is 0 and Asteroid is 1")]
    [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private float _spawnDelay = 5f;
    private bool _stopSpawning = false;

    [Header("Enemy Boundaries")]
    [SerializeField] private float _horizontalMin = -9.18f;
    [SerializeField] private float _horizontalMax = 9.33f;
    [SerializeField] private float _verticalMin = -4f;
    [SerializeField] private float _verticalMax = 6.5f;

    private void Awake()
    {

        //StartCoroutine(SpawnRoutine(_spawnDelay, _enemyNumber));
        StartCoroutine(SpawnRoutine(_spawnDelay));

    }

    //spawn every 5 second
    //Create a Coroutines of type IEnumerator -- Yield Events
    //use while loop

    IEnumerator SpawnRoutine(float _delay)//, int _enemyNumber)
    {
        
        int i = 0;

        while (_stopSpawning == false)
        {
            //random spawning at horizontal (x) for enemy
            float rdmHorizontal = Random.Range(_horizontalMin, _horizontalMax);
            Vector3 spawnPos = new Vector3(rdmHorizontal, _verticalMax, 0f);
            
            //enemy index 0 and asteroid index 1
            GameObject enemyClone = Instantiate(_enemyPrefab[0], spawnPos, Quaternion.identity, this.transform);
            enemyClone.transform.name = _enemyPrefab[0].name + " " + i++;
            
            //random spawning at horizontal (x) for asteroid
            float randomAsteroidPos = Random.Range(_horizontalMin, _horizontalMax);
            Vector3 asteroidSpawnPos = new Vector3(randomAsteroidPos, _verticalMax, 0f);
            //enemy index 0 and asteroid index 1
            GameObject asteroidClone = Instantiate(_enemyPrefab[1], asteroidSpawnPos, Quaternion.identity, this.transform);
            asteroidClone.transform.name = _enemyPrefab[1].name + " " + i++;

            yield return new WaitForSeconds(_delay);

        }

    }

    public void OnPlayerDead()
    {

        _stopSpawning = true;

    }

}
