using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("powerup mechanics")]
    [SerializeField]
    private float _movementSpd = 3.0f;
    [SerializeField] private float _activeTime = 5f;

    [Header("powerup Boundaries")]
    [SerializeField] private float _horizontalMin = -9.18f;
    [SerializeField] private float _horizontalMax = 9.33f;
    [SerializeField] private float _verticalMin = -3.9f;
    [SerializeField] private float _verticalMax = 6f;
    

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
            playerScript.TripleShootActivation(_activeTime);
            Destroy(this.gameObject);

        }

    }

}
