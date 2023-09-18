using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UB
{
    public class Abilities : MonoBehaviour
    {
        PlayerManager playerManager;
        PlayerStats playerStats;

        public Ability[] abilities;

        [Header("Ability 1")]
        //from ability
        public bool isSkillShot1 = true;
        public float ability1Cooldown = 5;
        public float mana1Cost = 5f;
        //from hud
        public Image ability1Image;
        public TextMeshProUGUI ability1CooldownText;
        public Canvas ability1Canvas;
        public Image ability1SkillShot;
        public float maxAbility1Distance; 

        [Header("Ability 2")]
        public bool isSkillShot2 = true;
        public Image ability2Image;
        public TextMeshProUGUI ability2CooldownText;
        public float ability2Cooldown = 5;
        public float mana2Cost = 5f;

        public Canvas ability2Canvas;
        public Image ability2SkillShot;
        public float maxAbility2Distance;

        [Header("Ability 3")]
        public bool isSkillShot3 = true;
        public Image ability3Image;
        public TextMeshProUGUI ability3CooldownText;
        public float ability3Cooldown = 5;
        public float mana3Cost = 3f;

        public Canvas ability3Canvas;
        public Image ability3SkillShot;
        public float maxAbility3Distance;

        private bool isAbility1Cooldown = false;
        private bool isAbility2Cooldown = false;
        private bool isAbility3Cooldown = false;

        private float currentAbility1Cooldown;
        private float currentAbility2Cooldown;
        private float currentAbility3Cooldown;

        private Vector3 position;
        private RaycastHit hit;
        private Ray ray;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            playerStats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(PlayerUIManager.instance != null)
            {
                AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, ability1Image, ability1CooldownText);
                AbilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2Cooldown, ability2Image, ability2CooldownText);
                AbilityCooldown(ref currentAbility3Cooldown, ability3Cooldown, ref isAbility3Cooldown, ability3Image, ability3CooldownText);

                Ability1Canvas();
                Ability2Canvas();
                Ability3Canvas();
            }   
        }

        public void ResetAbilities()
        {
            //menu
            ability1Image.fillAmount = 0;
            ability2Image.fillAmount = 0;
            ability3Image.fillAmount = 0;

            ability1CooldownText.text = "";
            ability2CooldownText.text = "";
            ability3CooldownText.text = "";

            ability1SkillShot.enabled = false;
            ability2SkillShot.enabled = false;
            ability3SkillShot.enabled = false;

            ability1Canvas.enabled = false;
            ability2Canvas.enabled = false;
            ability3Canvas.enabled = false;

            //values
            isSkillShot1 = abilities[0].isSkillShot;
            mana1Cost = abilities[0].manaCost;
            ability1Cooldown = abilities[0].abilityCooldown;


        }

        public bool CheckForAbilityInput()
        {
            if (ability1SkillShot.enabled && !isAbility1Cooldown)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }

                Quaternion lookRotaion = Quaternion.LookRotation(position - transform.position);
                lookRotaion.eulerAngles = new Vector3(0, lookRotaion.eulerAngles.y, lookRotaion.eulerAngles.z);
                GetComponent<AbilityActions>().shootRotation = lookRotaion;

                transform.rotation = Quaternion.Lerp(lookRotaion, transform.rotation, 0);

                isAbility1Cooldown = true;
                currentAbility1Cooldown = ability1Cooldown;

                ability1Canvas.enabled = false;
                ability1SkillShot.enabled = false;

                playerManager.playerAnimationManager.PlayTargetAnimation("Ability_01", true, false);

                PlayerStats.instance.DecreaseMana(Mathf.RoundToInt(mana1Cost));

                return true;
            }
            else if (ability2SkillShot.enabled && !isAbility2Cooldown)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }

                Quaternion lookRotaion = Quaternion.LookRotation(position - transform.position);
                lookRotaion.eulerAngles = new Vector3(0, lookRotaion.eulerAngles.y, lookRotaion.eulerAngles.z);

                transform.rotation = Quaternion.Lerp(lookRotaion, transform.rotation, 0);

                isAbility2Cooldown = true;
                currentAbility2Cooldown = ability2Cooldown;

                ability2Canvas.enabled = false;
                ability2SkillShot.enabled = false;

                playerManager.playerAnimationManager.PlayTargetAnimation("Ability_02", true, false);

                PlayerStats.instance.DecreaseMana(Mathf.RoundToInt(mana2Cost));

                return true;
            }
            else if (ability3SkillShot.enabled && !isAbility3Cooldown)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }

                Quaternion lookRotaion = Quaternion.LookRotation(position - transform.position);
                lookRotaion.eulerAngles = new Vector3(0, lookRotaion.eulerAngles.y, lookRotaion.eulerAngles.z);

                transform.rotation = Quaternion.Lerp(lookRotaion, transform.rotation, 0);

                isAbility3Cooldown = true;
                currentAbility3Cooldown = ability3Cooldown;

                ability3Canvas.enabled = false;
                ability3SkillShot.enabled = false;

                playerManager.playerAnimationManager.PlayTargetAnimation("Ability_03", true, false);

                PlayerStats.instance.DecreaseMana(Mathf.RoundToInt(mana3Cost));

                return true;
            }

            return false;
        }

        public void Ability1Input()
        {
            if (!isAbility1Cooldown && playerStats.currentMana >= mana1Cost)
            {
                ability2Canvas.enabled = false;
                ability2SkillShot.enabled = false;
                ability3Canvas.enabled = false;
                ability3SkillShot.enabled = false;

                if (isSkillShot1)
                {
                    playerManager.RemoveFocus();
                    ability1Canvas.enabled = true;
                    ability1SkillShot.enabled = true;
                }
                else
                {
                    isAbility1Cooldown = true;
                    currentAbility1Cooldown = ability1Cooldown;

                    playerManager.playerAnimationManager.PlayTargetAnimation("Ability_01", true, false);

                    PlayerStats.instance.DecreaseMana(Mathf.RoundToInt(mana1Cost));
                }
            }
        }

        public void Ability2Input()
        {
            if (!isAbility2Cooldown && playerStats.currentMana >= mana1Cost)
            {
                ability1Canvas.enabled = false;
                ability1SkillShot.enabled = false;
                ability3Canvas.enabled = false;
                ability3SkillShot.enabled = false;

                if (isSkillShot2)
                {
                    playerManager.RemoveFocus();
                    ability2Canvas.enabled = true;
                    ability2SkillShot.enabled = true;
                    playerManager.playerAnimationManager.PlayTargetAnimation("Ability_02_Part_1", true, false);
                }
                else
                {
                    isAbility2Cooldown = true;
                    currentAbility2Cooldown = ability2Cooldown;

                    playerManager.playerAnimationManager.PlayTargetAnimation("Ability_02", true, false);

                    PlayerStats.instance.DecreaseMana(Mathf.RoundToInt(mana2Cost));
                }
            }
        }

        public void Ability3Input()
        {
            if (!isAbility3Cooldown && playerStats.currentMana >= mana1Cost)
            {
                ability1Canvas.enabled = false;
                ability1SkillShot.enabled = false;
                ability2Canvas.enabled = false;
                ability2SkillShot.enabled = false;

                if (isSkillShot3)
                {
                    playerManager.RemoveFocus();
                    ability3Canvas.enabled = true;
                    ability3SkillShot.enabled = true;
                }
                else
                {
                    isAbility3Cooldown = true;
                    currentAbility3Cooldown = ability3Cooldown;

                    playerManager.playerAnimationManager.PlayTargetAnimation("Ability_03", true, false);

                    PlayerStats.instance.DecreaseMana(Mathf.RoundToInt(mana3Cost));
                }
            }
        }

        public void Ability4Input()
        {
            if (!isAbility3Cooldown)
            {
                playerManager.RemoveFocus();
                isAbility3Cooldown = true;
                currentAbility3Cooldown = ability3Cooldown;
            }
        }

        private void Ability1Canvas()
        {
            if (ability1SkillShot.enabled)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }

                Quaternion ab1Canvas = Quaternion.LookRotation(position - transform.position);
                ab1Canvas.eulerAngles = new Vector3(0, ab1Canvas.eulerAngles.y, ab1Canvas.eulerAngles.z);

                ability1Canvas.transform.rotation = Quaternion.Lerp(ab1Canvas, ability1Canvas.transform.rotation, 0);
            }
        }

        private void Ability2Canvas()
        {
            if (ability2SkillShot.enabled)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }

                Quaternion ab2Canvas = Quaternion.LookRotation(position - transform.position);
                ab2Canvas.eulerAngles = new Vector3(0, ab2Canvas.eulerAngles.y, ab2Canvas.eulerAngles.z);

                ability2Canvas.transform.rotation = Quaternion.Lerp(ab2Canvas, ability2Canvas.transform.rotation, 0);
            }
        }

        private void Ability3Canvas()
        {
            if (ability3SkillShot.enabled)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                }

                Quaternion ab3Canvas = Quaternion.LookRotation(position - transform.position);
                ab3Canvas.eulerAngles = new Vector3(0, ab3Canvas.eulerAngles.y, ab3Canvas.eulerAngles.z);

                ability3Canvas.transform.rotation = Quaternion.Lerp(ab3Canvas, ability3Canvas.transform.rotation, 0);
            }
        }

        private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image abilityImage, TextMeshProUGUI abilityText)
        {
            if (isCooldown)
            {
                currentCooldown -= Time.deltaTime;

                if(currentCooldown <= 0f)
                {
                    isCooldown = false;
                    currentCooldown = 0f;

                    if(abilityImage != null)
                    {
                        abilityImage.fillAmount = 0f;
                    }
                    if(abilityText.text != null)
                    {
                        abilityText.text = "";
                    }
                }
                else
                {
                    if (abilityImage != null)
                    {
                        abilityImage.fillAmount = currentCooldown / maxCooldown;
                    }
                    if(abilityText != null)
                    {
                        abilityText.text = Mathf.Ceil(currentCooldown).ToString();
                    }
                }
            }
        }
    }
}

