using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class MainMenuHUDManager : MonoBehaviour
    {
        public void StartNewGame()
        {
            WorldSceneManager.instance.StartNewGame();
        }
    }
}

