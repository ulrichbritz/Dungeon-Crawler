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

        [Header("Other UI")]
        [SerializeField] private TextMeshProUGUI damageTakenText;
        [SerializeField] private GameObject goldGiveUI;
        [SerializeField] private TextMeshProUGUI goldGivenText;

        [Header("Kill Values")]
        [SerializeField] private int minCoinValue;
        [SerializeField] private int maxCoinValue;

        protected override void Awake()
        {
            base.Awake();

            UpdateResourceUI();

            playerCameraManager = PlayerCameraManager.instance;
        }

        protected override void Update()
        {

            if (playerCameraManager != null)
            {
                cameraDirection = playerCameraManager.cameraObject.transform.forward;
                cameraDirection.y = 0;

                resourcesCanvas.transform.rotation = Quaternion.LookRotation(cameraDirection);
            }
            else
            {
                playerCameraManager = PlayerCameraManager.instance;
            }
        }

        public override void TakeDamage(int _physicalDamage, int _magicalDamage)
        {
            base.TakeDamage(_physicalDamage, _magicalDamage);

            StartCoroutine(DisplayEnemyDamageTaken(lastDamageTaken));

            UpdateResourceUI();
        }

        public override void Die()
        {
            base.Die();

            DamageCollider[] damageColliders = GetComponentsInChildren<DamageCollider>();

            foreach(DamageCollider damageCollider in damageColliders)
            {
                damageCollider.OnDisable();
            }

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

            Outline outline = GetComponent<Outline>();
            if(outline != null)
            {
                outline.enabled = false;
            }

            int coinAmountToGive = Random.Range(minCoinValue, maxCoinValue);
            StartCoroutine(DisplayGoldGiven(coinAmountToGive));
            PlayerManager.instance.playerInventoryManager.GetGold(coinAmountToGive);
            GetComponent<EnemySoundFXManager>().PlayGiveGoldSFX(1);

            StartCoroutine(DestroyMe());
        }

        private void UpdateResourceUI()
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }

        IEnumerator DestroyMe()
        {
            yield return new WaitForSeconds(0.5f);

            resourcesCanvas.SetActive(false);

            yield return new WaitForSeconds(3.5f);

            Destroy(gameObject);
        }

        IEnumerator DisplayEnemyDamageTaken(int damageTaken)
        {
            damageTakenText.text = damageTaken.ToString();

            yield return new WaitForSeconds(0.5f);

            damageTakenText.text = "";

        }

        IEnumerator DisplayGoldGiven(int goldGiven)
        {
            goldGiveUI.SetActive(true);
            goldGivenText.text = "+" + goldGiven.ToString();  

            yield return new WaitForSeconds(0.5f);

            goldGiveUI.SetActive(false);
        }

        
    }

}

