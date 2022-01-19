using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyDatas
{

    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Add New Character")]
    public class EnemyDatas : ScriptableObject
    {

        #region enemy_profile
        [Header("Enemy Profile")]

        public string enemy_name;

        #endregion

        #region enemy_action

        [Header("Action Variable")]

        [SerializeField]
        private float enemy_spd;

        [SerializeField]
        private float atkDmg;

        [SerializeField]
        private int enemyHP;

        [SerializeField]
        private float bulletSpeed;

        [SerializeField]
        private float _fireRate = 2f;

        [SerializeField]
        private float _canFire = -1f;

        #endregion

        #region 3DMODEL

        [Header("3D Model")]

        [SerializeField]
        private GameObject charPrefab;

        #endregion

    }

}
