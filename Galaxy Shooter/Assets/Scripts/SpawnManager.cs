using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [Header("Enemy Spawner")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnDelay = 5f;
    //[SerializeField] private int _enemyNumber = 5;
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

            float rdmHorizontal = Random.Range(_horizontalMin, _horizontalMax);
            Vector3 spawnPos = new Vector3(rdmHorizontal, _verticalMax, 0f);
            GameObject enemyClone = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity, this.transform);
            enemyClone.transform.name = _enemyPrefab.name + " " + i++;
            yield return new WaitForSeconds(_delay);
            //_enemyNumber--;

        }

    }

    public void OnPlayerDead()
    {

        _stopSpawning = true;

    }

}
