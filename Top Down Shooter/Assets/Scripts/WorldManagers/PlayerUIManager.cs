using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class PlayerUIManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static PlayerUIManager instance;

        [Header("Scripts")]
        [HideInInspector] public PlayerManager playerManager;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            playerManager = PlayerManager.instance;

            DontDestroyOnLoad(gameObject);
        }
    }
}

