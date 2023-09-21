using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class ProjectileDamageCollider : DamageCollider
    {
        BasicProjectile basicProjectile;
        AbilityProjectile abilityProjectile;

        protected override void Awake()
        {
            base.Awake();

            basicProjectile = GetComponent<BasicProjectile>();
            abilityProjectile = GetComponent<AbilityProjectile>();

            OnEnable();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.gameObject.layer == PlayerManager.instance.movementMask.value)
            {
                Destroy(gameObject);
            }

            if (GetComponent<BasicProjectile>() != null)
            {
                if (other.transform == GetComponent<BasicProjectile>().target)
                {
                    CharacterStats characterStats = other.gameObject.GetComponent<CharacterStats>();

                    if (characterStats != null)
                    {
                        Instantiate(bloodPrefab, transform.position, Quaternion.identity);
                        characterStats.TakeDamage(basicProjectile.carriedPhysicalDamage, basicProjectile.carriedMagicalDamage);
                        Destroy(gameObject);
                    }
                }
            }   
            
            if (GetComponent<AbilityProjectile>() != null)
            {
                EnemyStats characterStats = other.gameObject.GetComponent<EnemyStats>();

                if (characterStats != null)
                {
                    Instantiate(bloodPrefab, transform.position, Quaternion.identity);
                    //characterStats.TakeDamage(abilityProjectile.carriedPhysicalDamage, abilityProjectile.carriedMagicalDamage);
                    characterStats.TakeDamage(abilityProjectile.carriedPhysicalDamage, Mathf.RoundToInt(PlayerManager.instance.abilities.abilities[0].magicalDamage));
                    Destroy(gameObject);
                }
            }


        }
    }
}

