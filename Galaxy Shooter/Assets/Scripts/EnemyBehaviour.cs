using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [Header("Enemy Mechanic")]
    [SerializeField]
    private float _speed = 4.0f;
    private bool _isEnemyDead = false;

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
    private Animator _anim;

    [Header("Sound Effect")]
    //[SerializeField] private AudioClip _laserSFX;
    [SerializeField] private AudioClip _explosionSFX;

    private void Awake()
    {

        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        if (_anim == null)
        {
            _anim = this.gameObject.GetComponent<Animator>();
        }
                
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

            EnemyDestroyedAnimation();

        }

        if (col.tag == "Shield")
        {

           if (player != null && _gm != null)
            {

                player.ShieldHit();
                _gm.AddScore();

            }

            Debug.Log("Shield Destroyed");
            EnemyDestroyedAnimation();
        
        }

    }


    public void EnemyDestroyedAnimation()
    {

        _isEnemyDead = true;
        ExplosionSFX();

        this.GetComponent<BoxCollider>().enabled = false;
        _anim.SetTrigger("EnemyDead");

        if (this.transform.position.y < _verticalMin && _isEnemyDead == true)
        {

            Destroy(this.gameObject);

        }

        Destroy(this.gameObject, 2.30f);        

    }

    void ExplosionSFX()
    {

        AudioSource.PlayClipAtPoint(_explosionSFX, this.transform.position, 1f);

    }


    private void EnemyMovement()
    {

        //random transform position on x axis
        float rdmHorizontal = Random.Range(_horizontalMin, _horizontalMax);
        //speed of moving down
        this.transform.Translate(Vector3.down.normalized * _speed * Time.deltaTime);
        //if trigger the border
        if (this.transform.position.y < _verticalMin && _isEnemyDead == false)
        {
            //set the position to the random horizontal and vertical
            this.transform.position = new Vector3(rdmHorizontal, _verticalMax, 0);

        }

    }

}
