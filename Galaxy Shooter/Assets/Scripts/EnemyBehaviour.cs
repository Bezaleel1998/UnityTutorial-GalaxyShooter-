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
    [SerializeField] private float _verticalMin = -4f;
    [SerializeField] private float _verticalMax = 6.5f;

    [Header("Action Variable")]
    private int _enemyHP = 1;

    [Header("Other Game Object")]
    private GameManager _gm;
    private Player player;

    private void Awake()
    {

        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    private void FixedUpdate()
    {

        EnemyMovement();

    }

    //if you want to convert it to 2D just use OnTriggerEnter2D(Collider2D col)
    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player")
        {

            //damage player
            //destroy us
            //Player player = col.GetComponent<Player>();

            if (player != null && _gm != null)
            {
                player.Damage();
                _gm.AddScore();
            }

            Destroy(this.gameObject);

        }

        if (col.tag == "Shield")
        {

           if (player != null && _gm != null)
            {

                player.ShieldHit();
                _gm.AddScore();

            }

            Debug.Log("Shield Destroyed");
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
