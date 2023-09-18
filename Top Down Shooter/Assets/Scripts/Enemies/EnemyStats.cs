using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UB
{
    public class EnemyStats : CharacterStats
    {
        [Header("Resource Bars")]
        [SerializeField] private GameObject resourcesCanvas;
        [SerializeField] private Slider hpSlider;
        private PlayerCameraManager playerCameraManager;
        private Vector3 cameraDirection;

        protected override void Awake()
        {
            base.Awake();

            UpdateResourceUI();

            playerCameraManager = PlayerCameraManager.instance;
        }

        private void Update()
        {
            if(playerCameraManager != null)
            {
                cameraDirection = playerCameraManager.cameraObject.transform.forward;
                cameraDirection.y = 0;

                resourcesCanvas.transform.rotation = Quaternion.LookRotation(cameraDirection);
            }
            else
            {
                playerCameraManager = PlayerCameraManager.instance;
            }
            

            //resourcesCanvas.transform.LookAt(new Vector3(playerCameraManager.transform.position.x, 0, playerCameraManager.transform.position.z));
        }

        public override void TakeDamage(int _physicalDamage, int _magicalDamage)
        {
            base.TakeDamage(_physicalDamage, _magicalDamage);

            UpdateResourceUI();
        }

        public override void Die()
        {
            base.Die();

            

            EnemyManager enemyManager = GetComponent<EnemyManager>();
            if(enemyManager != null)
            {
                MobSpawner.instance.CheckIfWasLastEnemy(enemyManager);
                enemyManager.enabled = false;
            }

            Interactable interactable = GetComponent<Interactable>();
            if(interactable != null)
            {
                Destroy(interactable);
            }

            characterManager.characterAnimationManager.PlayTargetAnimation("Death_01", true, true);
            characterManager.navMeshAgent.isStopped = true;

            resourcesCanvas.SetActive(false);

            Outline outline = GetComponent<Outline>();
            if(outline != null)
            {
                outline.enabled = false;
            }

            StartCoroutine(DestroyMe());
        }

        private void UpdateResourceUI()
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }

        IEnumerator DestroyMe()
        {
            yield return new WaitForSeconds(4f);

            Destroy(gameObject);
        }
    }

}

