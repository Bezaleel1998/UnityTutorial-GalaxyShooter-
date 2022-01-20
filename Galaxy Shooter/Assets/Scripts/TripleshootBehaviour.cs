using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleshootBehaviour : MonoBehaviour
{

    public void SetAllChildAttackDamage(float _atkDmg)
    {

        foreach (Transform child in transform)
        {

            child.GetComponent<LaserBehaviour>().SetDefault(_atkDmg);

        }

        Destroy(this.gameObject, 2f);

    }

}
