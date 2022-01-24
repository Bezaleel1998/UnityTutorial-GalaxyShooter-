using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Powerup mechanics")]
    [SerializeField]
    private float _movementSpd = 3.0f;
    //ID for PowerUps
    //0 = triple shoot, 1 = speedUp, 2 = Shields
    [SerializeField]
    private int _powerUpID;

    [Header("Powerup Boundaries")]
    [SerializeField] private float _horizontalMin = -9.18f;
    [SerializeField] private float _horizontalMax = 9.33f;
    [SerializeField] private float _verticalMin = -3.9f;
    [SerializeField] private float _verticalMax = 6f;

    [Header("Powerup Tripleshoot Variable")]
    [SerializeField] private float _activeTimeTripleShoot = 5f;

    [Header("Powerup SpeedUp Variable")]
    [SerializeField] private float _activeTimeSpeedUp = 5f;
    [SerializeField] private float _speedMultiplier = 2f;

    [Header("Shield Barrier Variable")]
    [SerializeField] private float _shieldActiveTime = 5f;

    void Update()
    {

        PowerUpBehaviour(_movementSpd);

    }

    void PowerUpBehaviour(float spd)
    {

        //movedown at speed of 3 (can be adjust at inspector)
        //when we leave the border destroy this
        float rdmHorizontal = Random.Range(_horizontalMin, _horizontalMax);
        this.transform.Translate(Vector3.down.normalized * spd * Time.deltaTime);

        if (this.transform.position.y < _verticalMin)
        {

            Destroy(this.gameObject);

        }


    }


    //if you want to convert it to 2D just use OnTriggerEnter2D(Collider2D col)
    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player")
        {

            Player playerScript = col.GetComponent<Player>();

            switch (_powerUpID)
            {

                case 0:
                                        
                    playerScript.TripleShootActivation(_activeTimeTripleShoot);
                    Debug.Log("TripleShoot activated");
                    
                    break;

                case 1:

                    playerScript.SpeedPowerUpActivation(_speedMultiplier, _activeTimeSpeedUp);
                    Debug.Log("SpeedUp activated");
                    
                    break;

                case 2:

                    playerScript.ShieldActive(_shieldActiveTime);
                    Debug.Log("Shield activated");

                    break;

                default:
                    Debug.LogError("This Object has null parameter");
                    break;
            }

            Destroy(this.gameObject);

        }

    }

}
