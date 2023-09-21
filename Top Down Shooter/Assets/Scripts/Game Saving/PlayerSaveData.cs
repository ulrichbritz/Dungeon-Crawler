using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    [System.Serializable]
    // reference this data for ever save file
    public class PlayerSaveData
    {
        [Header("Time Played")]
        public float secondsPlayed;

        [Header("Player Upgrades")]
        public int damageUpgrade;
        public int healthUpgrade;
    }
}

