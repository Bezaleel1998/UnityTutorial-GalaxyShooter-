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

    [Header("Player Movement")]
    private float _horizontalInput;
    private float _verticalInput;

    [Header("Player Boundaries")]
    [SerializeField] private float _horizontalMin = -9.18f;
    [SerializeField] private float _horizontalMax = 9.33f;
    [SerializeField] private float _verticalMin = -3.9f;
    [SerializeField] private float _verticalMax = 6f;

    [Header("Action Variable")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _parentBullet;
    [SerializeField] private float _selfDestructTime = 2f;
    [SerializeField] private float _fireRate = .8f;
    private float _canFire = -1f;
    internal float _attackDmg;
    internal float _playerHP;

    #endregion

    private void Awake()
    {

        //take the current position = new position(0, 0, 0)
        this.transform.position = new Vector3(0, 0, 0);
               
    }

    void FixedUpdate()
    {

        PlayerMovement();

    }

    void Update()
    {

        PlayerAttack(_bulletPrefab, _parentBullet, _selfDestructTime);

    }

    private void LateUpdate()
    {
        Boundaries();
    }


    void PlayerMovement()
    {

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        //new Vector3(x, y, z) * horizontal input (-1 or 1) * speed of movement * real time
        
        Vector3 dir = new Vector3(_horizontalInput, _verticalInput, 0);
        this.transform.Translate(dir.normalized * _speed * Time.deltaTime);

    }

    void Boundaries()
    {

        //Mathf.Clamp(what coordinate that you want to clamp, min val, max val)
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, _horizontalMin, _horizontalMax), 
                Mathf.Clamp(this.transform.position.y, _verticalMin, _verticalMax), this.transform.position.z);

    }


    void PlayerAttack(GameObject bPrefab, GameObject parent, float selfDestructTimer)
    {
        //when space key is pressed
        //GetKeyDown is for once at a time
        //GetKey is for holding the button
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            //for fire rate / cooldown system
            _canFire = Time.time + _fireRate;

            //spawn bullet
            Vector3 bulletOffset = new Vector3(0, 1f, 0);
            GameObject clonePrefabs = Instantiate(bPrefab, transform.position + bulletOffset, Quaternion.identity, parent.transform);

            //self destruct after amount of time
            //Destroy(clonePrefabs, selfDestructTimer);

        }

    }

}
