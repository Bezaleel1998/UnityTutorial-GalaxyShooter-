using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Spawner")]
    [SerializeField] private GameObject _enemyPrefab;

    [Header("Enemy Boundaries")]
    [SerializeField] private float _horizontalMin = -9.18f;
    [SerializeField] private float _horizontalMax = 9.33f;
    [SerializeField] private float _verticalMin = -3.9f;
    [SerializeField] private float _verticalMax = 6f;

    private void Awake()
    {

        EnemySpawner();

    }


    private void EnemySpawner()
    {

        float rdmHorizontal = Random.Range(_horizontalMin, _horizontalMax);
        Vector3 spawnPos = new Vector3(rdmHorizontal, _verticalMax, 0f);
        GameObject enemyClone = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity, this.transform);

    }


    

}
