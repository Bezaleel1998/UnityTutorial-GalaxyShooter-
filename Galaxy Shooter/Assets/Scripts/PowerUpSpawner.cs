using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Enemy Spawner")]
    [SerializeField] private GameObject _PowerUpPrefabs;
    [SerializeField] private float _spawnDelay = 10f;
    private bool _stopSpawning = false;

    [Header("Enemy Boundaries")]
    [SerializeField] private float _horizontalMin = -9.18f;
    [SerializeField] private float _horizontalMax = 9.33f;
    [SerializeField] private float _verticalMin = -4f;
    [SerializeField] private float _verticalMax = 6.5f;

    private void Awake()
    {

        StartCoroutine(SpawnRoutine(_spawnDelay));

    }

    IEnumerator SpawnRoutine(float _delay)
    {

        int i = 0;

        while (_stopSpawning == false)
        {

            float rdmHorizontal = Random.Range(_horizontalMin, _horizontalMax);
            Vector3 spawnPos = new Vector3(rdmHorizontal, _verticalMax, 0f);
            
            GameObject powerUpClone = Instantiate(_PowerUpPrefabs, spawnPos, Quaternion.identity, this.transform);
            powerUpClone.transform.name = _PowerUpPrefabs.name + " " + i++;

            yield return new WaitForSeconds(Random.Range(3, _delay));

        }

    }

    public void OnPlayerDead()
    {

        _stopSpawning = true;

    }
}
