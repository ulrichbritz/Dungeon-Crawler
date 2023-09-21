using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UB
{
    public class GameMessageHUD : MonoBehaviour
    {
        public static GameMessageHUD instance;

        [SerializeField] private TextMeshProUGUI gameMessageText;

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
        }

        public IEnumerator DisplayGameMessage(string message)
        {
            gameMessageText.text = "";
            gameMessageText.text = message;

            yield return new WaitForSeconds(3f);

            gameMessageText.text = "";
        }
    }
}

