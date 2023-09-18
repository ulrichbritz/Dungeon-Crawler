using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class MeleeDamageCollider : DamageCollider
    {
        [Header("Scripts")]
        CharacterStats wielderStats;

        protected override void Awake()
        {
            base.Awake();

            OnDisable();

            wielderStats = GetComponentInParent<CharacterStats>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if(other.gameObject.transform == target)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if(targetStats != null)
                {
                    Instantiate(bloodPrefab, transform.position, Quaternion.identity);
                    targetStats.TakeDamage(Mathf.RoundToInt(wielderStats.GetPhysicalDamage()), Mathf.RoundToInt(wielderStats.GetMagicalDamage()));
                }
            }  
        }

        

    }
}

