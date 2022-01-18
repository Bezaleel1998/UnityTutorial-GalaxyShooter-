using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerDatas
{

    [CreateAssetMenu(fileName = "Player", menuName = "Player/Add New Character")]
    public class PlayerDatas : ScriptableObject
    {

        #region player_profile
        [Header("Player Profile")]

        public string player_name;

        #endregion

        #region player_action

        [Header("Action Variable")]

        [SerializeField]
        internal float player_speed;

        [SerializeField]
        internal float atkDmg;

        [SerializeField]
        internal float playerHP;

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
        internal GameObject charPrefab;

        #endregion


    }

}
