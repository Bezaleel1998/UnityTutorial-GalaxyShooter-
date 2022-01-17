using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerDatas
{

    [CreateAssetMenu(fileName = "Player", menuName = "Player/Add New Character")]
    public class PlayerDatas : ScriptableObject
    {

        #region player_profile

        public string player_name;

        public float player_speed;

        #endregion


    }

}
