using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class AbilityCollider : DamageCollider
    {
        AbilityProjectile abilityProjectile;

        protected override void Awake()
        {
            base.Awake();

            abilityProjectile = GetComponent<AbilityProjectile>();

            OnEnable();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.GetComponent<EnemyStats>() != null)
            {
                EnemyStats enemyStats = other.GetComponent<EnemyStats>();
                //enemyStats.TakeDamage(abilityProjectile.carriedPhysicalDamage, abilityProjectile.carriedMagicalDamage);
                enemyStats.TakeDamage(abilityProjectile.carriedPhysicalDamage, Mathf.RoundToInt(PlayerManager.instance.abilities.abilities[1].magicalDamage));
            }
        }


    }
}

