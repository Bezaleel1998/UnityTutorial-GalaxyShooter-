using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private and Internal VARIABLE
    //public, private or internal reference
    //data type (float, int, bool, string, etc)
    //every variable has a name
    //optional value assigned

    [Header("Player Mechanic")]
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private SpawnManager _spManager;
    [SerializeField] private PowerUpSpawner _powerUpManager;

    [Header("Player Movement")]
    private float _horizontalInput;
    private float _verticalInput;

    [Header("Player Boundaries")]
    [SerializeField] private float _horizontalMin = -9.18f;
    [SerializeField] private float _horizontalMax = 9.33f;
    [SerializeField] private float _verticalMin = -3.9f;
    [SerializeField] private float _verticalMax = 6f;

    [Header("Action Variable")]
    [SerializeField] private GameObject[] _bulletPrefab;
    [SerializeField] private GameObject _parentBullet;
    [SerializeField] private float _fireRate = .8f;
    private float _canFire = -1f;
    private float _attackDmg = 2f;
    private int _playerHP = 3;
    [SerializeField] private bool _isTripleShootEnabled = false;
    [SerializeField] private Vector3 bulletOffset = new Vector3(0, 1f, 0);

    #endregion

    private void Awake()
    {

        PlayerSpawn();

        if (_spManager == null)
        {

            //spManager = gameObject.GetComponent(typeof(SpawnManager)) as SpawnManager;
            _spManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();

        }

        if (_powerUpManager == null)
        {

            _powerUpManager = GameObject.FindGameObjectWithTag("PowerUpSpawner").GetComponent<PowerUpSpawner>();

        }

    }

    void FixedUpdate()
    {

        PlayerMovement();

    }

    void Update()
    {

        PlayerAttack();

    }

    private void LateUpdate()
    {
        Boundaries();
    }


    #region PlayerSpawn,Damage,AndMovement

    private void PlayerSpawn()
    {

        //spawn Player3D
        //take the current position = new position(0, 0, 0)
        GameObject playerChar = Instantiate(_playerPrefab, transform.position, Quaternion.identity, this.transform);
        playerChar.transform.name = _playerPrefab.name;

        this.transform.position = new Vector3(0, -4, 0);

    }

    public void Damage()
    {

        _playerHP--;

        //if life is 0 then player destroyed
        if (_playerHP <= 0)
        {

            //Show GameOverUI
            //Destroy Player3D inside this code (this parent)
            foreach (Transform child in this.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            Debug.Log("Player Destroyed");
            //communicated with spawn manager 
            //let them know to stop running
            _spManager.OnPlayerDead();
            _powerUpManager.OnPlayerDead();
            //pause the game

        }
        else
        {

            Debug.Log("Player Life = " + _playerHP);

        }

    }

    void PlayerMovement()
    {

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        //new Vector3(x, y, z) * horizontal input (-1 or 1) * speed of movement * real time

        Vector3 dir = new Vector3(_horizontalInput, _verticalInput, 0);
        this.transform.Translate(dir.normalized * _speed * Time.deltaTime);

    }

    #endregion

    #region PlayerAttackCode

    void PlayerAttack()
    {
        
        //GetKeyDown is for once at a time
        //GetKey is for holding the button
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {

            //for fire rate / cooldown system
            _canFire = Time.time + _fireRate;

            if (_isTripleShootEnabled == true)
            {

                //index 1 for triple shoot laser
                TripleShootLaserFire(_bulletPrefab[1], _parentBullet, _attackDmg);

            }
            else
            {
                //index 0 for normal shoot laser
                FireLaser(_bulletPrefab[0], _parentBullet, _attackDmg);

            }
            
        }

    }

    void FireLaser(GameObject bPrefab, GameObject parent, float _dmg)
    {

        //spawn bullet
        GameObject clonePrefabs = Instantiate(bPrefab, transform.position + bulletOffset, Quaternion.identity, parent.transform);

        //set the attack damage
        clonePrefabs.GetComponent<LaserBehaviour>().SetDefault(_dmg);

    }

    void TripleShootLaserFire(GameObject triplePrefabs, GameObject parent, float _dmg)
    { 

        //spawn bullet
        GameObject clonePrefabs = Instantiate(triplePrefabs, transform.position + bulletOffset, Quaternion.identity, parent.transform);

        //set the attack damage
        clonePrefabs.GetComponent<TripleshootBehaviour>().SetAllChildAttackDamage(_dmg);

    }

    public void TripleShootActivation(float activationTime)
    {

        _isTripleShootEnabled = true;
        //Start Counting down coroutine for triple shoot
        StartCoroutine(TripleShootPowerDownRoutine(activationTime));

    }

    IEnumerator TripleShootPowerDownRoutine(float activationTime)
    {
        
        yield return new WaitForSeconds(activationTime);
        _isTripleShootEnabled = false;

    }


    #endregion

    #region BorderOfGameplay

    void Boundaries()
    {

        //Mathf.Clamp(what coordinate that you want to clamp, min val, max val)
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, _horizontalMin, _horizontalMax),
                Mathf.Clamp(this.transform.position.y, _verticalMin, _verticalMax), this.transform.position.z);

    }

    #endregion

}
