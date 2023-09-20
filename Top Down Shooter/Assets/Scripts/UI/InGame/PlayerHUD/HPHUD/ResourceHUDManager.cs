using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UB
{
    public class ResourceHUDManager : MonoBehaviour
    {
        [Header("Scripts")]
        private PlayerManager playerManager;
        private PlayerStats playerStats;

        [Header("Components")]
        [SerializeField] private Slider hpSlider;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private Slider mpSlider;
        [SerializeField] private TextMeshProUGUI mpText;

        private void Awake()
        {

        }

        private void Start()
        {
            playerManager = PlayerManager.instance;
            playerStats = PlayerStats.instance;

            playerStats.onHPChangedCallback += UpdateResourceUI;
            playerStats.onManaChangedCallback += UpdateResourceUI;

            UpdateResourceUI();
        }

        public void UpdateResourceUI()
        {
            hpSlider.maxValue = playerStats.GetMaxHealth();
            hpSlider.value = playerStats.currentHealth;
            hpText.text = $"{playerStats.currentHealth}/{playerStats.GetMaxHealth()}";

            mpSlider.maxValue = playerStats.GetMaxMana();
            mpSlider.value = playerStats.currentMana;
            mpText.text = $"{playerStats.currentMana}/{playerStats.GetMaxMana()}";
        }


    }
}

