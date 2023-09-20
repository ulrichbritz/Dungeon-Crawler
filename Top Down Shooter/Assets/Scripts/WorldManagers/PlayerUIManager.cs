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

        [Header("UI Sections")]
        public GameObject playerHUD;
        public GameObject shopHUD;

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

        public void OpenShop()
        {
            shopHUD.SetActive(true);
            playerManager.RemoveFocus();
            playerManager.hasUIOpen = true;
        }

        public void CloseShop()
        {
            shopHUD.SetActive(false);
            playerManager.hasUIOpen = false;
        }

    }
}

