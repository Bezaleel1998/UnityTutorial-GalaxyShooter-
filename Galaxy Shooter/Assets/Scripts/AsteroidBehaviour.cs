using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{

    [Header("Asteroid Mechanic")]
    [SerializeField]
    private float _speed = 1.0f;
    private float _rotationSpeed;
    private bool _isAsteroidDead = false;

    [Header("Other Game Object")]
    private GameManager _gm;
    //private Player _player;
    [SerializeField] private GameObject _explosionPrefab;

    [Header("Asteroid Boundaries")]
    [SerializeField] private float _horizontalMin = -9.18f;
    [SerializeField] private float _horizontalMax = 9.33f;
    [SerializeField] private float _verticalMin = -4f;
    [SerializeField] private float _verticalMax = 6.5f;

    [Header("Sound Effect")]
    [SerializeField] private AudioClip _explosionSFX;

    void Awake()
    {

        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        _rotationSpeed = Random.Range(-22f, 22f);


    }

    void FixedUpdate()
    {

        AsteroidMovement();

    }

    void AsteroidMovement()
    {
        //rotation of the asteroid
        this.transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);

        this.transform.Translate(Vector3.down.normalized * _speed * Time.deltaTime);

    }

    public void AsteroidDestroyed()
    {

        GameObject explosion = Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
        this.GetComponent<SphereCollider>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject, 2.30f);
        Destroy(explosion.gameObject, 2.30f);
        
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player")
        {

            ExplosionSFX();
            _gm.AddScore(20);
            col.GetComponent<Player>().Damage();
            AsteroidDestroyed();

        }

        if (col.tag == "Player2")
        {

            ExplosionSFX();
            _gm.AddScore(20);
            col.GetComponent<Player>().Damage();
            AsteroidDestroyed();

        }

        if (col.tag == "Shield")
        {

            col.transform.parent.GetComponent<Player>().ShieldHit();
            _gm.AddScore(20);
            ExplosionSFX();
            AsteroidDestroyed();

        }

    }

    public void ExplosionSFX()
    {

        AudioSource.PlayClipAtPoint(_explosionSFX, this.transform.position, 1f);

    }

    void Border()
    {

        if (this.transform.position.y < _verticalMin && _isAsteroidDead == false)
        {
            Destroy(this.gameObject);
        }
        else if (this.transform.position.y < _verticalMax && _isAsteroidDead == false)
        {
            Destroy(this.gameObject);
        }

        if (this.transform.position.x < _horizontalMin && _isAsteroidDead == false)
        {
            Destroy(this.gameObject);
        }
        else if(this.transform.position.x < _horizontalMax && _isAsteroidDead == false)
        {
            Destroy(this.gameObject);
        }

    }

}
