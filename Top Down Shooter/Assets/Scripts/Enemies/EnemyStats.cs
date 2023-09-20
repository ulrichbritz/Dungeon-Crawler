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

            StartCoroutine(DisplayEnemyDamageTaken(lastDamageTaken));

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

            Outline outline = GetComponent<Outline>();
            if(outline != null)
            {
                outline.enabled = false;
            }

            int coinAmountToGive = Random.Range(minCoinValue, maxCoinValue);
            StartCoroutine(DisplayGoldGiven(coinAmountToGive));
            PlayerManager.instance.playerStats.GetCoins(coinAmountToGive);
            GetComponent<EnemySoundFXManager>().PlayGiveCoinsSFX(1);

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

