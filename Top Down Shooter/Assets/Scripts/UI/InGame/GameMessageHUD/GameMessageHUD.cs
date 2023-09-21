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
            gameMessageText.gameObject.SetActive(true);
            gameMessageText.text = "";
            gameMessageText.text = message;

            yield return new WaitForSeconds(2f);

            gameMessageText.text = "";
            gameMessageText.gameObject.SetActive(false);
        }

        public void HideGameMessage()
        {
            gameMessageText.text = "";
            gameMessageText.gameObject.SetActive(false);
        }
    }
}

