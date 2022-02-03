using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    #region Private and Internal VARIABLE
    //public, private or internal reference
    //data type (float, int, bool, string, etc)
    //every variable has a name
    //optional value assigned

    [Header("Player Mechanic")]
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Animator _player_animator;

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
    [SerializeField] private Vector3 bulletOffset = new Vector3(0, 1f, 0);
    [SerializeField] private bool _isTripleShootEnabled = false;
    [SerializeField] private bool _isSpeedUpEnabled = false;
    [SerializeField] private bool _isShieldEnabled = false;
    [SerializeField] internal bool _isPlayerDefeat = false;
    private GameObject playerChar;

    [Header("Other variable")]
    [SerializeField] private GameManager gM;
    [SerializeField] private GameObject _shieldGameObject;
    [SerializeField] private GameObject[] _fireDamage;
    [SerializeField] private GameObject _explosionPrefab;

    [Header("Sound Effect")]
    [SerializeField] private AudioClip _laserSFX;
    [SerializeField] private AudioClip _explosionSFX;


    #endregion

    private void Awake()
    {

        if (gM == null)
        {
            gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        PlayerSpawn();

        _shieldGameObject.SetActive(false);

        if (_player_animator == null)
        {
            _player_animator = playerChar.GetComponent<Animator>();
        }        

    }

    void FixedUpdate()
    {
        PlayerMechanic();
    }

    private void LateUpdate()
    {
        Boundaries();
    }


    #region PlayerSpawn,Damage,AndMovement

    #region PlayerSpawn

    private void PlayerSpawn()
    {

        //spawn Player3D
        //take the current position = new position(0, 0, 0)
        playerChar = Instantiate(_playerPrefab, transform.position, Quaternion.identity, this.transform);
        playerChar.transform.name = _playerPrefab.name;

        if (gM._isCoOp_Mode == false)
        {

            this.transform.position = new Vector3(0, -4, 0);

        }

    }

    #endregion

    #region Visual and Sound Effect

    void ExplosionSFX()
    {

        AudioSource.PlayClipAtPoint(_explosionSFX, this.transform.position, 1f);

    }

    void VisualDamageAnimation(int playerLives)
    {

        switch (playerLives)
        {
            case 0:
                for (int i = 0; i < _fireDamage.Length; i++)
                {
                    _fireDamage[i].gameObject.SetActive(false);
                }
                break;

            case 1:
                for (int i = 0; i < _fireDamage.Length; i++)
                {
                    _fireDamage[i].gameObject.SetActive(true);
                }
                break;

            case 2:
                _fireDamage[0].gameObject.SetActive(true);
                break;

            default:
                break;
        }

    }

    #endregion

    #region PlayerLives

    public void Damage()
    {

        _playerHP--;

        VisualDamageAnimation(_playerHP);
        VisualUICaller();

        //if life is 0 then player destroyed
        if (_playerHP <= 0)
        {

            PlayerDestroyed();
            Debug.Log("Player Has Been Destroyed");

            if (gM._isCoOp_Mode == false)
            {

                gM._isGameOver = true;

            }

            _isPlayerDefeat = true;

        }

    }

    void VisualUICaller()
    {

        if (gM._isCoOp_Mode == true)
        {

            if (this.tag == "Player")
            {
                gM.PlayerLiveIndicator(_playerHP, 0);
            }
            else
            {
                gM.PlayerLiveIndicator(_playerHP, 1);
            }

        }
        else
        {
            gM.PlayerLiveIndicator(_playerHP, 0);
        }

    }

    private void PlayerMechanic()
    {
                
        if (_playerHP > 0)
        {

            PlayerMovement();
            PlayerAttack();
            ShieldStatus();

            if (gM._isCoOp_Mode == false)
            {
                gM._isGameOver = false;
            }

            _isPlayerDefeat = false;

        }

    }

    void PlayerDestroyed()
    {

        //Destroy Player3D inside this code (this parent)
        Destroy(playerChar);
        ExplosionSFX();
        //VFX
        GameObject explosionClone = Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity, this.transform);
        Destroy(explosionClone.gameObject, 2.30f);
        this.GetComponent<BoxCollider>().enabled = false;

    }

    #endregion

    #region PlayerMovement

    void PlayerMovement()
    {

        //RuntimePlatformDetection();

        if (gM._isCoOp_Mode == true)
        {

            MultiplayerDetection();

        }
        else
        {
            DesktopMovement();
        }

        //new Vector3(x, y, z) * horizontal input (-1 or 1) * speed of movement * real time
        Vector3 dir = new Vector3(_horizontalInput, _verticalInput, 0);
        this.transform.Translate(dir.normalized * _speed * Time.deltaTime);

    }


    void RuntimePlatformDetection()
    {

        //detect if this is mobile or desktop application
#if UNITY_ANDROID
        AndroidMovement();
#elif UNITY_STANDALONE_WIN
        DesktopMovement();
#endif

    }


    void MultiplayerDetection()
    {

        if (this.tag == "Player2")
        {

            Player2DesktopMovement();

        }
        else
        {

            DesktopMovement();

        }

    }


    void Player2DesktopMovement()
    {

        _horizontalInput = Input.GetAxis("Mouse X");
        _verticalInput = Input.GetAxis("Mouse Y");

        if (_horizontalInput > 0)
        {

            _player_animator.SetBool("TurnRight", true);

        }
        else if (_horizontalInput < 0)
        {

            _player_animator.SetBool("TurnLeft", true);

        }
        else
        {

            _player_animator.SetBool("TurnRight", false);
            _player_animator.SetBool("TurnLeft", false);

        }

    }


    void DesktopMovement()
    {

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (_horizontalInput > 0)
        {

            _player_animator.SetBool("TurnRight", true);

        }
        else if (_horizontalInput < 0)
        {

            _player_animator.SetBool("TurnLeft", true);

        }
        else
        {

            _player_animator.SetBool("TurnRight", false);
            _player_animator.SetBool("TurnLeft", false);

        }

    }

    void AndroidMovement()
    {

        _horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        _verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

        if (_horizontalInput > 0)
        {

            _player_animator.SetBool("TurnRight", true);

        }
        else if (_horizontalInput < 0)
        {

            _player_animator.SetBool("TurnLeft", true);

        }
        else
        {

            _player_animator.SetBool("TurnRight", false);
            _player_animator.SetBool("TurnLeft", false);

        }

    }

    public void SpeedPowerUpActivation(float speedMultiplier, float timeActivation)
    {

        _isSpeedUpEnabled = true;
        //increase _speed of the player
        float normalSpeed = _speed;
        _speed = _speed * speedMultiplier;

        //set up the coroutine for timing of the ability
        StartCoroutine(SpeedUpActivationTime(timeActivation, normalSpeed));

    }

    IEnumerator SpeedUpActivationTime(float activeTime, float normalSpeed)
    {

        yield return new WaitForSeconds(activeTime);
        _speed = normalSpeed;

        _isSpeedUpEnabled = false;

    }

    #endregion

    #endregion

    #region PlayerAttackCode

    void PlayerAttack()
    {

        //GetKeyDown is for once at a time
        //GetKey is for holding the button

#if UNITY_ANDROID
        if (CrossPlatformInputManager.GetButton("Fire") && Time.time > _canFire)
        {

            //for fire rate / cooldown system
            _canFire = Time.time + _fireRate;

            ShootCommence();

        }
#elif UNITY_STANDALONE_WIN

        if (gM._isCoOp_Mode == true)
        {

            if (this.tag == "Player2")
            {

                Player2Attack();

            }
            else
            {

                Player1Attack();

            }

        }
        else
        {
            Player1Attack();
        }

#endif

    }

    void Player1Attack()
    {

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {

            //for fire rate / cooldown system
            _canFire = Time.time + _fireRate;

            ShootCommence();

        }

    }

    void Player2Attack()
    {
        
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > _canFire)
        {

            //for fire rate / cooldown system
            _canFire = Time.time + _fireRate;

            ShootCommence();

        }

    }

    void ShootCommence()
    {

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

        LaserSFX();

    }

    void LaserSFX()
    {

        AudioSource.PlayClipAtPoint(_laserSFX, this.transform.position, 1f);

    }

    void FireLaser(GameObject bPrefab, GameObject parent, float _dmg)
    {

        //spawn bullet
        GameObject clonePrefabs = Instantiate(bPrefab, transform.position + bulletOffset, Quaternion.identity, parent.transform);

        //set the attack damage
        clonePrefabs.GetComponent<LaserBehaviour>().SetDefault(_dmg);

        //set _fromplayer as true
        clonePrefabs.GetComponent<LaserBehaviour>()._isLaserFromPlayer(true);

    }

    void TripleShootLaserFire(GameObject triplePrefabs, GameObject parent, float _dmg)
    { 

        //spawn bullet
        GameObject clonePrefabs = Instantiate(triplePrefabs, transform.position + bulletOffset, Quaternion.identity, parent.transform);

        //set the attack damage
        clonePrefabs.GetComponent<TripleshootBehaviour>().SetAllChildAttackDamage(_dmg, true);

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

    #region ShieldPowerUp

    public void ShieldActive(float timeActivation)
    {

        _isShieldEnabled = true;
        StartCoroutine(ShieldActiveTime(timeActivation));

    }

    public void ShieldHit()
    {

        _isShieldEnabled = false;

    }

    void ShieldStatus()
    {

        
        if (_isShieldEnabled == true)
        {

            _shieldGameObject.SetActive(true);
            Debug.Log("ShieldDeployed");

        }
        else
        {
            
            _shieldGameObject.SetActive(false);

        }

    }

    IEnumerator ShieldActiveTime(float timeActivation)
    {

        yield return new WaitForSeconds(timeActivation);
        _isShieldEnabled = false;

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
