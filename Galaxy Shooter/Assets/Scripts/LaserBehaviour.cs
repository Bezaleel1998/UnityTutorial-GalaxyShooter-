using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{

    #region Private and Internal Variable

    [Header("Laser Mechanic")]
    [SerializeField] private float _bulletSpeed = 8f;
    [SerializeField] private float _bulletHeight = 7f;
    [SerializeField] private float _damageAmnt = 1f;
    //[SerializeField] private float _selfDestructTime = 2f;
    [SerializeField] private GameManager _gameManager;

    #endregion

    private void Awake()
    {

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

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

        if (this.transform.position.y > _bulletHeight)
        {
            Destroy(this.gameObject);
        }

        //self destruct after amount of time
        //Destroy(clonePrefabs, _selfDestructTime);

    }

    //if you want to convert it to 2D just use OnTriggerEnter2D(Collider2D col)
    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Enemy")
        {

            //add player score
            _gameManager.AddScore();
            //Destroy enemy Game Object
            EnemyBehaviour eB = col.GetComponent<EnemyBehaviour>();
            eB.EnemyDestroyedAnimation();
            //Destroy us
            Destroy(this.gameObject);

        }

        if (col.tag == "Player")
        {

            //damage player
            //destroyUs
            //Destroy(this.gameObject);

        }

        if (col.tag == "Asteroid")
        {
            
            //destroy Asteroid
            AsteroidBehaviour aB = col.GetComponent<AsteroidBehaviour>();
            aB.AsteroidDestroyed();
            //DestroyUs
            Destroy(this.gameObject);

        }

    }

}
