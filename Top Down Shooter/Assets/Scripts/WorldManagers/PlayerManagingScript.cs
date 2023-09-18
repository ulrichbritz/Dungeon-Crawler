using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class PlayerManagingScript : MonoBehaviour
    {
        public static PlayerManagingScript instance;

        public GameObject player;

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

            player = PlayerManager.instance.gameObject;
        }
    }
}

