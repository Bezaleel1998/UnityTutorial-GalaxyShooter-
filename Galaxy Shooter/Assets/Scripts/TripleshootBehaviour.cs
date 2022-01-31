using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleshootBehaviour : MonoBehaviour
{

    public void SetAllChildAttackDamage(float _atkDmg, bool _fromPlayer)
    {

        foreach (Transform child in transform)
        {

            child.GetComponent<LaserBehaviour>().SetDefault(_atkDmg);
            child.GetComponent<LaserBehaviour>()._isLaserFromPlayer(_fromPlayer);

        }

        Destroy(this.gameObject, 2f);

    }

}
