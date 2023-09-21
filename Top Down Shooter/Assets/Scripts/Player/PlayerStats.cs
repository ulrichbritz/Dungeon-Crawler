using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class PlayerStats : CharacterStats
    {
        public static PlayerStats instance;

        [Header("Scripts")]
        private PlayerManager playerManager;

        //CallBacks
        public delegate void OnHPChanged();
        public OnHPChanged onHPChangedCallback;
        public delegate void OnManaChanged();
        public OnManaChanged onManaChangedCallback;

        protected override void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            playerManager = GetComponent<PlayerManager>();

            base.Awake();   
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Start()
        {
            base.Start();
        }



        public override void TakeDamage(int _physicalDamage, int _magicalDamage)
        {
            base.TakeDamage(_physicalDamage, _magicalDamage);

            playerManager.playerSFXManager.PlayHurtSFX(0.5f);

             if (onHPChangedCallback != null)
               onHPChangedCallback.Invoke();
        }

        public override void DecreaseMana(int _manaToDecrease)
        {
            base.DecreaseMana(_manaToDecrease);

            if (onManaChangedCallback != null)
                onManaChangedCallback.Invoke();
        }

        public override void IncreaseMana(int _manaToIncrease)
        {
            base.IncreaseMana(_manaToIncrease);

            if (onManaChangedCallback != null)
                onManaChangedCallback.Invoke();
        }

        public override int GetMaxHealth()
        {
            int maxHealthWithModifiers = maxHealth;
            foreach (Item item in playerManager.playerInventoryManager.items)
            {
                maxHealthWithModifiers += item.health;
            }

            return maxHealthWithModifiers;
        }

        public override int GetMaxMana()
        {
            int maxManaWithModifiers = maxMana;
            foreach (Item item in playerManager.playerInventoryManager.items)
            {
                maxManaWithModifiers += item.mana;
            }

            return maxManaWithModifiers;
        }

        public override float GetPhysicalDamage(float extraPhysicalDamage = 0)
        {
            float physicalDamageWithModifiers = physicalDamage;
            foreach(Item item in playerManager.playerInventoryManager.items)
            {
                physicalDamageWithModifiers += item.physicalDamage;
            }

            physicalDamageWithModifiers += extraPhysicalDamage;

            return physicalDamageWithModifiers;
        }

        public override float GetMagicalDamage(float extraMagicalDamage = 0)
        {
            float magicalDamageWithModifiers = magicalDamage;
            foreach (Item item in playerManager.playerInventoryManager.items)
            {
                magicalDamageWithModifiers += item.magicalDamage;
            }

            extraMagicalDamage += extraMagicalDamage;

            return magicalDamageWithModifiers;
        }

        public override float GetAttackTime()
        {
            float attackTime = baseAttackTime / (1 + (GetAttackSpeed() / 100));
            return attackTime;
        }

        public override float GetAttackSpeed()
        {
            float attackSpeedWithModifiers = attackSpeed;
            foreach (Item item in playerManager.playerInventoryManager.items)
            {
                attackSpeedWithModifiers += item.attackSpeed;
            }

            return attackSpeedWithModifiers;
        }

        public override float GetAttackRange()
        {
            float attackRangeWithModifiers = attackRange;
            foreach (Item item in playerManager.playerInventoryManager.items)
            {
                attackRangeWithModifiers += item.attackRange;
            }

            return attackRangeWithModifiers;
        }

        public override float GetArmor()
        {
            float armorWithModifiers = armor;
            foreach (Item item in playerManager.playerInventoryManager.items)
            {
                armorWithModifiers += item.armor;
            }

            if (armorWithModifiers >= 100)
                return 100;
            else
                return armorWithModifiers;
        }

        public override float GetMagicResistance()
        {
            float magicResistanceWithModifiers = magicResistance;
            foreach (Item item in playerManager.playerInventoryManager.items)
            {
                magicResistanceWithModifiers += item.magicResistance;
            }

            if (magicResistanceWithModifiers >= 100)
                return 100;
            else
                return magicResistanceWithModifiers;
        }

        public override float GetMoveSpeed()
        {
            return base.GetMoveSpeed();
        }

        


    }
}

