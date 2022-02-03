using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{

    #region Private and Internal Variable

    [Header("Laser Mechanic")]
    [SerializeField] private float _bulletSpeed = 8f;
    [SerializeField] private float _damageAmnt = 1f;
    //[SerializeField] private float _selfDestructTime = 2f;
    [SerializeField] private GameManager _gameManager;
    private bool _fromPlayer;

    [Header("Laser Boundaries")]
    [SerializeField] private float _horizontalMin = -9.18f;
    [SerializeField] private float _horizontalMax = 9.33f;
    [SerializeField] private float _verticalMin = -7f;
    [SerializeField] private float _verticalMax = 7f;

    /*[Header("Game Object")]
    [SerializeField]
    private Player _playerScript;*/

    #endregion

    private void Awake()
    {

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //_playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    public void SetDefault(float _dmg)
    {

        _damageAmnt = _dmg;
        
    }

    void FixedUpdate()
    {

        BulletMovement();

    }

    void BulletMovement()
    {

        this.transform.Translate(Vector3.up.normalized * _bulletSpeed * Time.deltaTime);

        if (_fromPlayer)
        {

            if (this.transform.position.y > _verticalMax)
            {
                Destroy(this.gameObject);
            }

        }
        else
        {

            if (this.transform.position.y < _verticalMin)
            {
                Destroy(this.gameObject);
            }

        }

        //self destruct after amount of time
        //Destroy(this, _selfDestructTime);

    }

    //if you want to convert it to 2D just use OnTriggerEnter2D(Collider2D col)
    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Enemy")
        {

            if (_fromPlayer)
            {

                //add player score
                _gameManager.AddScore(10);
                //Destroy enemy Game Object
                EnemyBehaviour eB = col.GetComponent<EnemyBehaviour>();
                eB.EnemyDestroyedAnimation();
                //Destroy us
                Destroy(this.gameObject);

            }

        }

        if (col.tag == "Player")
        {

            if (!_fromPlayer)
            {

                //damage player
                col.GetComponent<Player>().Damage();
                //destroyUs
                Destroy(this.gameObject);

            }

        }

        if (col.tag == "Player2")
        {

            if (!_fromPlayer)
            {

                //damage player
                col.GetComponent<Player>().Damage();
                //destroyUs
                Destroy(this.gameObject);

            }

        }

        if (col.tag == "Shield")
        {

            if (!_fromPlayer)
            {

                col.transform.parent.GetComponent<Player>().ShieldHit();
                Destroy(this.gameObject);

            }

        }

        if (col.tag == "Asteroid")
        {

            if (_fromPlayer)
            {

                _gameManager.AddScore(20);
                //destroy Asteroid
                AsteroidBehaviour aB = col.GetComponent<AsteroidBehaviour>();
                aB.AsteroidDestroyed();
                aB.ExplosionSFX();
                //DestroyUs
                Destroy(this.gameObject);

            }

        }

    }

    public bool _isLaserFromPlayer(bool _isTrue)
    {

        return _fromPlayer = _isTrue;

    }

}
