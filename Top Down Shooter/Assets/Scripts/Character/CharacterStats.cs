using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("Scripts")]
        protected CharacterManager characterManager;

        [Header("Resources")]
        public int maxHealth;
        public int currentHealth { get; private set; }
        public int maxMana;
        public int currentMana { get; private set; }

        [Header("Damage")]
        public float physicalDamage;
        public float magicalDamage;

        [Header("AttackSpeed")]
        public float baseAttackTime;
        public float attackSpeed = 100;

        [Header("AttackRange")]
        public float attackRange;

        [Header("Armor")]
        public float armor;
        public float magicResistance;

        [Header("Speed")]
        public float moveSpeed;

        [SerializeField] private float manaRegenSpeed = 1f;
        private float manaRegenTimer = 0f;

        [Header("Blood")]
        [SerializeField] private GameObject bloodPrefab;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();      
        }

        protected virtual void Start()
        {
            currentHealth = Mathf.RoundToInt(GetMaxHealth());
            currentMana = Mathf.RoundToInt(GetMaxMana());
        }

        protected virtual void Update()
        {
            if (currentMana < maxMana)
            {
                manaRegenTimer -= Time.deltaTime;

                if (manaRegenTimer <= 0)
                {
                    IncreaseMana(1);
                    manaRegenTimer = manaRegenSpeed;
                }
            }
        }

        public virtual void TakeDamage(int _physicalDamage, int _magicalDamage)
        {
            //figure out physical damage after physical resist from armor
            float physicalResist;
            if (GetArmor() >= 100)
            {
                physicalResist = 1;
            }               
            else if (GetArmor() >= 1)
            {
                physicalResist = (GetArmor()) / 100;
            }
            else
            {
                physicalResist = 0;
            }


            int totalPhysicalDamage =  Mathf.RoundToInt(_physicalDamage * (1 - physicalResist));

            //figure out magical damage after magic resist
            float magicResist;
            if(GetMagicResistance() >= 100)
            {
                magicResist = 1;
            }
            else if (GetMagicResistance() >= 1)
            {
                magicResist = GetMagicResistance() / 100;
            }
            else
            {
                magicResist = 0;
            }

            int totalMagicalDamage = Mathf.RoundToInt(_magicalDamage * (1 - magicResist));

            //add total physical and magical damage together
            int totalDamage = totalPhysicalDamage + totalMagicalDamage;

            //do damage to health
            currentHealth -= Mathf.Clamp(totalDamage, 0 , int.MaxValue);
            Debug.Log(transform.name + " takes " + totalDamage + " damage");

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            Debug.Log(transform.name + " died!");
        }

        public virtual void DecreaseMana(int _manaToDecrease)
        {
            currentMana -= _manaToDecrease;
            Debug.Log(transform.name + " uses " + _manaToDecrease + " mana");
        }

        public virtual void IncreaseMana(int _manaToIncrease)
        {
            currentMana += _manaToIncrease;
        }

        #region Getting Stats

        public virtual int GetMaxHealth()
        {
            return maxHealth;
        }
        public virtual int GetMaxMana()
        {
            return maxMana;
        }

        public virtual float GetPhysicalDamage(float extraPhysicalDamage = 0)
        {
            return physicalDamage;
        }

        public virtual float GetMagicalDamage(float extraMagicalDamage = 0)
        {
            return magicalDamage;
        }

        public virtual float GetAttackTime()
        {
            float attackTime = baseAttackTime / (attackSpeed / 100);
            return attackTime;
        }

        public virtual float GetAttackSpeed()
        {
            return attackSpeed;
        }

        public virtual float GetAttackRange()
        {
            return attackRange;
        }

        public virtual float GetArmor()
        {
            return armor;
        }

        public virtual float GetMagicResistance()
        {
            return magicResistance;
        }

        public virtual float GetMoveSpeed()
        {
            return moveSpeed;
        }



        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, GetAttackRange());
        }
    }
}

