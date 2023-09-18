using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    [RequireComponent(typeof(CharacterStats))]
    public class EnemyInteractable : Interactable
    {
        [Header("Scripts")]
        private CharacterStats myStats;

        protected override void Awake()
        {
            base.Awake();

            myStats = GetComponent<CharacterStats>();
        }

        protected override void Start()
        {
            base.Start();

            raduis = PlayerManagingScript.instance.player.GetComponent<CharacterStats>().GetAttackRange();
        }

        protected override void Update()
        {
            base.Update();

            if (isFocus)
            {
                float distance = Vector3.Distance(playerTransform.position, interactionTransform.position);
                if (distance <= raduis)
                {
                    Interact();
                }
            }
        }

        public override void Interact()
        {
            base.Interact();

            CharacterCombatManager attackerCombatManager = PlayerManagingScript.instance.player.transform.GetComponent<CharacterCombatManager>();

            //attack enemy
            if (attackerCombatManager != null)
            {
                attackerCombatManager.Attack(myStats);
            }
        }
    }
}

