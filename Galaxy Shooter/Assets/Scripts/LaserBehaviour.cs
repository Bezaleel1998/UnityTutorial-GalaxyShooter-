using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{

    #region Private and Internal Variable

    [Header("Laser Mechanic")]
    [SerializeField] private float _bulletSpeed = 8f;
    [SerializeField] private float _bulletHeight = 7f;

    #endregion

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

    }

}
