using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UB
{
    public class AbilityHUDManager : MonoBehaviour
    {
        private PlayerManager playerManager;

        [Header("Ability 1")]
        public Image darkImage1;
        public Image lightImage1;
        //send to abilities
        public Image ability1Icon;
        public TextMeshProUGUI ability1CooldownText;

        public Canvas ability1Canvas;
        public Image ability1SkillShot;

        [Header("Ability 2")]
        public Image darkImage2;
        public Image lightImage2;
        //send to abilities
        public Image ability2Icon;
        public TextMeshProUGUI ability2CooldownText;

        public Canvas ability2Canvas;
        public Image ability2SkillShot;

        [Header("Ability 3")]
        public Image darkImage3;
        public Image lightImage3;
        //send to abilities
        public Image ability3Icon;
        public TextMeshProUGUI ability3CooldownText;

        public Canvas ability3Canvas;
        public Image ability3SkillShot;

        private void Awake()
        {
            playerManager = PlayerManager.instance;

            playerManager.abilities.ability1Image = ability1Icon;
            playerManager.abilities.ability1CooldownText = ability1CooldownText;

            playerManager.abilities.ability2Image = ability2Icon;
            playerManager.abilities.ability2CooldownText = ability2CooldownText;

            playerManager.abilities.ability3Image = ability3Icon;
            playerManager.abilities.ability3CooldownText = ability3CooldownText;

            playerManager.abilities.ResetAbilities();

            //from ability itself
            darkImage1.sprite = playerManager.abilities.abilities[0].abilityIcon;
            lightImage1.sprite = playerManager.abilities.abilities[0].abilityIcon;
            //from ability itself
            darkImage2.sprite = playerManager.abilities.abilities[1].abilityIcon;
            lightImage2.sprite = playerManager.abilities.abilities[1].abilityIcon;
            //from ability itself
            darkImage3.sprite = playerManager.abilities.abilities[2].abilityIcon;
            lightImage3.sprite = playerManager.abilities.abilities[2].abilityIcon;
        }
    }
}

