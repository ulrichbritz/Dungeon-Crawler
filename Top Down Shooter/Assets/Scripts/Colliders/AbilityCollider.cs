using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class AbilityCollider : DamageCollider
    {
        protected override void Awake()
        {
            base.Awake();

            OnEnable();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<EnemyStats>() != null)
            {
                EnemyStats enemyStats = other.GetComponent<EnemyStats>();
                enemyStats.TakeDamage(0 , Mathf.RoundToInt(PlayerManager.instance.playerStats.GetMagicalDamage(PlayerManager.instance.abilities.abilities[1].magicalDamage)));
            }
        }


    }
}

