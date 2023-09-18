using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class AbilityActions : MonoBehaviour
    {
        [SerializeField] GameObject ability1Projectile;
        [SerializeField] Transform ability1SpawnPoint;

        [HideInInspector] public Quaternion shootRotation;

        protected virtual void Awake()
        {

        }

        public virtual void PerformAbility1(GameObject projectilePrefab = null)
        {

        }

        public virtual void PerformAbility2(GameObject abilityPrefab = null)
        {

        }

        public virtual void PerformAbility3()
        {
            
        }

        public virtual void PerformAbility4()
        {

        }
    }
}

