using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UB
{
    public class WorldSceneManager : MonoBehaviour
    {
        public static WorldSceneManager instance;

        public int mainMenuIndex = 0;
        public int characterSelectionIndex = 1;
        public int safeSpaceRoomIndex = 2;

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

            DontDestroyOnLoad(gameObject);
        }

        public void StartNewGame()
        {
            SceneManager.LoadScene(characterSelectionIndex);
        }

        public void LoadGame()
        {

        }

        public void LoadScene(int sceneIndex)
        {
            PlayerManager.instance.navMeshAgent.enabled = false;

            SceneManager.LoadScene(sceneIndex);       
        }

        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(mainMenuIndex);
        }

    }
}

