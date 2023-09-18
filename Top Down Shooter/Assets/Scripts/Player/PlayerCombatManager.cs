using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        [Header("Scripts")]
        private PlayerInventoryManager playerInventoryManager;

        protected override void Awake()
        {
            base.Awake();

            playerInventoryManager = GetComponent<PlayerInventoryManager>();
        }

        protected override void Start()
        {
            base.Start();

            playerInventoryManager.onItemChangedCallback += UpdateAttackTime;
        }

        protected override void UpdateAttackTime()
        {
            base.UpdateAttackTime();
        }
    }
}

