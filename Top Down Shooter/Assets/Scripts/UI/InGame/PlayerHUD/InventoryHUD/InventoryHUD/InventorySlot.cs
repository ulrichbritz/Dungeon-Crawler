using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UB
{
    public class InventorySlot : MonoBehaviour
    {
        [Header("Item Slot Components")]
        public Image icon;
        public Button removeButton;

        [Header("Item Details")]
        [SerializeField] TextMeshProUGUI physicalDamageText;
        [SerializeField] TextMeshProUGUI magicalDamageText;
        [SerializeField] TextMeshProUGUI attackSpeedText;
        [SerializeField] TextMeshProUGUI attackRangeText;
        [SerializeField] TextMeshProUGUI armorText;
        [SerializeField] TextMeshProUGUI magicResistText;
        [SerializeField] TextMeshProUGUI moveSpeedText;

        [HideInInspector] public Item item;

        public void AddItem(Item newItem)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
            removeButton.interactable = true;

            physicalDamageText.text = $"Physical Damage = {item.physicalDamage}";
            magicalDamageText.text = $"Magical Damage = {item.magicalDamage}";
            attackSpeedText.text = $"Attack Speed = {item.attackSpeed}";
            attackRangeText.text = $"Attack Range = {item.attackRange}";
            armorText.text = $"Armor = {item.armor}";
            magicResistText.text = $"Magic Resist = {item.magicResistance}";
            moveSpeedText.text = $"Move Speed = {item.moveSpeed}";

        }

        public void ClearSlot()
        {
            item = null;

            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
        }

        public void OnRemoveButton()
        {
            PlayerManager.instance.playerInventoryManager.Remove(item);
        }

        public void UseItem()
        {
            if(item != null)
            {
                item.Use();
            }
        }
    }
}

