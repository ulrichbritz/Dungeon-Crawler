using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UB
{
    [CreateAssetMenu (fileName = "New Ability", menuName = "Abilities/New Ability")]
    public class Ability : ScriptableObject
    {
        [Header("Ability Description")]
        public string abilityName;
        public string abilityDescription;

        [Header("Ability Menu")]
        public bool isSkillShot = true;
        public Sprite abilityIcon;
        public float abilityCooldown = 5;
        public float manaCost = 5f;

        [Header("Ability Stats")]
        public float physicalDamage;
        public float magicalDamage;
    }
}

