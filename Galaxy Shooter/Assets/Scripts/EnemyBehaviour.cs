using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [Header("Enemy Mechanic")]
    [SerializeField]
    private float _speed = 4.0f;

    [Header("Enemy Boundaries")]
    [SerializeField] private float _horizontalMin = -9.18f;
    [SerializeField] private float _horizontalMax = 9.33f;
    [SerializeField] private float _verticalMin = -3.9f;
    [SerializeField] private float _verticalMax = 6f;

    [Header("Action Variable")]
    private int _enemyHP = 1;

    private void FixedUpdate()
    {

        EnemyMovement();

    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player")
        {

            //damage player
            //destroy us
            
            Player player = col.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);

        }

    }



    private void EnemyMovement()
    {

        float rdmHorizontal = Random.Range(_horizontalMin, _horizontalMax);
        this.transform.Translate(Vector3.down.normalized * _speed * Time.deltaTime);

        if (this.transform.position.y < _verticalMin)
        {

            this.transform.position = new Vector3(rdmHorizontal, _verticalMax, 0);

        }

    }

}
