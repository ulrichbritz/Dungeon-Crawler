using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UB
{
    public class ShopItemSlot : MonoBehaviour
    {
        [Header("Item Slot Type")]
        public bool isGearSlot = true;
        public bool isHealingItemSlot = false;

        [Header("Item Slot Components")]
        public Image icon;

        [Header("Item Details")]
        [SerializeField] TextMeshProUGUI priceText;
        [SerializeField] TextMeshProUGUI physicalDamageText;
        [SerializeField] TextMeshProUGUI magicalDamageText;
        [SerializeField] TextMeshProUGUI attackSpeedText;
        [SerializeField] TextMeshProUGUI attackRangeText;
        [SerializeField] TextMeshProUGUI armorText;
        [SerializeField] TextMeshProUGUI magicResistText;
        [SerializeField] TextMeshProUGUI moveSpeedText;

        public Item item;

        private void Awake()
        {
            if(item != null)
                AddItem(item);
        }

        public void AddItem(Item newItem)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;

            priceText.text = item.itemShopValue.ToString();
            physicalDamageText.text = $"Physical Damage = {item.physicalDamage}";
            magicalDamageText.text = $"Magical Damage = {item.magicalDamage}";
            attackSpeedText.text = $"Attack Speed = {item.attackSpeed}";
            attackRangeText.text = $"Attack Range = {item.attackRange}";
            armorText.text = $"Armor = {item.armor}";
            magicResistText.text = $"Magic Resist = {item.magicResistance}";
            moveSpeedText.text = $"Move Speed = {item.moveSpeed}";

        }

        public void OnBuyButton()
        {
            //if has enough coin
            if(PlayerManager.instance.playerInventoryManager.goldAmount >= item.itemShopValue)
            {
                if (isGearSlot)
                {
                    if (PlayerManager.instance.playerInventoryManager.Add(item))
                    {
                        WorldSFXManager.instance.PlaySuccessFulItemPurchase(1);
                        PlayerManager.instance.playerInventoryManager.LoseGold(item.itemShopValue);
                    }
                    else
                    {
                        WorldSFXManager.instance.PlayGameErrorSFX(1);
                        StartCoroutine(GameMessageHUD.instance.DisplayGameMessage("Not enough room in inventory for item."));
                    }
                }
            }
            else
            {
                WorldSFXManager.instance.PlayGameErrorSFX(1);
                StartCoroutine(GameMessageHUD.instance.DisplayGameMessage("Not enough gold to purchase item."));
            }



        }
    }
}

