using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region VARIABLE
    //public, private or internal reference
    //data type (float, int, bool, string, etc)
    //every variable has a name
    //optional value assigned

    [Header("Player Mechanic")]
    [SerializeField]
    private float _speed = 5.0f;

    [Header("Player Movement")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Player Boundaries")]
    [SerializeField] private float horizontalMin = -9.18f;
    [SerializeField] private float horizontalMax = 9.33f;
    [SerializeField] private float verticalMin = -3.9f;
    [SerializeField] private float verticalMax = 6f;

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

    private void LateUpdate()
    {
        Boundaries();
    }


    void PlayerMovement()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //new Vector3(x, y, z) * horizontal input (-1 or 1) * speed of movement * real time
        
        Vector3 dir = new Vector3(horizontalInput, verticalInput, 0);
        this.transform.Translate(dir.normalized * _speed * Time.deltaTime);

    }

    void Boundaries()
    {

        //Mathf.Clamp(what coordinate that you want to clamp, min val, max val)
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, horizontalMin, horizontalMax), 
                Mathf.Clamp(this.transform.position.y, verticalMin, verticalMax), this.transform.position.z);

    }

}
