using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterCombatManager : MonoBehaviour
    {
        [Header("Scripts")]
        private CharacterManager characterManager;
        private CharacterStats myStats;

        [Header("Attack variables")]
        public Transform targetTransform;
        [SerializeField] private bool isMelee = false;
        [SerializeField] private GameObject rangedProjectilePrefab;
        [SerializeField] private GameObject rangedInstantiationPoint;
        private float attackTime;
        private float attackCooldown = 0;

        protected virtual void Awake()
        {
            myStats = GetComponent<CharacterStats>();
            characterManager = GetComponent<CharacterManager>();
            
        }

        protected virtual void Start()
        {
            UpdateAttackTime();
        }

        protected virtual void Update()
        {
            attackCooldown -= Time.deltaTime;
            print(attackCooldown);
        }

        public virtual void Attack(CharacterStats targetStats)
        {         
            targetTransform = targetStats.transform;
            DamageCollider damageCollider = GetComponentInChildren<DamageCollider>();
            if(damageCollider != null)
            {
                damageCollider.target = targetTransform;
            }

            if(attackCooldown <= 0f)
            {
                characterManager.characterAnimationManager.PlayTargetAnimation("Basic_Attack_01", true, false);
                
                attackCooldown = attackTime;
            }           
        }

        public void SpawnProjectile(GameObject projectilePrefab)
        {
            var projectile = Instantiate(projectilePrefab, rangedInstantiationPoint.transform.position, Quaternion.identity);
            BasicProjectile basicProjectile = projectile.GetComponent<BasicProjectile>();
            if (basicProjectile != null)
            {
                //get damage
                basicProjectile.carriedPhysicalDamage = Mathf.RoundToInt(myStats.GetPhysicalDamage());
                basicProjectile.carriedMagicalDamage = Mathf.RoundToInt(myStats.GetMagicalDamage());

                //get target
                basicProjectile.target = targetTransform;

                Interactable interactable = targetTransform.GetComponent<Interactable>();
                if(interactable != null)
                {
                    basicProjectile.targetFollow = interactable.interactionTransform;
                }
                else
                {
                    basicProjectile.targetFollow = targetTransform;
                }
            }
        }

        protected virtual void UpdateAttackTime()
        {
            attackTime = myStats.GetAttackTime();
        }
    }
}

