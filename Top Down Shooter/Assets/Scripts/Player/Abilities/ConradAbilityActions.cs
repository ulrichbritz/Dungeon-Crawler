using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class ConradAbilityActions : AbilityActions
    {
        private PlayerManager playerManager;

        [SerializeField] Transform abilityInstantiationPoint;
        [SerializeField] Transform weaponPoint;

        protected override void Awake()
        {
            base.Awake();

            playerManager = GetComponent<PlayerManager>();
        }

        public override void PerformAbility1(GameObject projectilePrefab)
        {
            base.PerformAbility1();

            
            var projectile = Instantiate(projectilePrefab, abilityInstantiationPoint.position , transform.rotation);
            AbilityProjectile abilityProjectile = projectile.GetComponent<AbilityProjectile>();
            if (abilityProjectile != null)
            {
                abilityProjectile.travelDistance = 7f;
                //get damage
                abilityProjectile.carriedPhysicalDamage = Mathf.RoundToInt(PlayerStats.instance.GetPhysicalDamage(playerManager.abilities.abilities[0].physicalDamage));
                abilityProjectile.carriedMagicalDamage = Mathf.RoundToInt(PlayerStats.instance.GetMagicalDamage(playerManager.abilities.abilities[0].magicalDamage));

                abilityProjectile.instantiationPoint = new Vector3(weaponPoint.position.x, abilityProjectile.transform.position.y, weaponPoint.transform.position.z);
                abilityProjectile.shootRotation = shootRotation;
            }
            
        }

        public override void PerformAbility2(GameObject abilityPrefab)
        {
            base.PerformAbility2();

            GameObject abilityFX = Instantiate(abilityPrefab, abilityInstantiationPoint.position, transform.rotation);

            AbilityProjectile abilityProjectile = abilityFX.GetComponent<AbilityProjectile>();
            if (abilityProjectile != null)
            {
                abilityProjectile.travelDistance = 0f;
                //get damage
                abilityProjectile.carriedPhysicalDamage = Mathf.RoundToInt(PlayerStats.instance.GetPhysicalDamage(playerManager.abilities.abilities[1].physicalDamage));
                abilityProjectile.carriedMagicalDamage = Mathf.RoundToInt(PlayerStats.instance.GetMagicalDamage(playerManager.abilities.abilities[1].magicalDamage));

                abilityProjectile.instantiationPoint = new Vector3(weaponPoint.position.x, abilityProjectile.transform.position.y, weaponPoint.transform.position.z);
                abilityProjectile.shootRotation = shootRotation;
            }

            StartCoroutine(DeleteSpawnedAbility(abilityFX, 0.5f));
        }

        public override void PerformAbility3()
        {
            base.PerformAbility3();

            StartCoroutine(DoAbility3());
        }

        public override void PerformAbility4()
        {
            base.PerformAbility4();
        }

        public IEnumerator DoAbility3()
        {
            PlayerManager.instance.navMeshAgent.speed = PlayerManager.instance.navMeshAgent.speed * 2f;

            yield return new WaitForSeconds(3f);

            PlayerManager.instance.navMeshAgent.speed = PlayerManager.instance.navMeshAgent.speed / 2f;
        }

        private IEnumerator DeleteSpawnedAbility(GameObject abilityToDelete, float time)
        {
            yield return new WaitForSeconds(time);

            Destroy(abilityToDelete);
        }

    }
}

