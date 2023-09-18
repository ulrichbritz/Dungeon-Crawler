using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class InventoryUIManager : MonoBehaviour
    {
        [Header("Scripts")]
        private PlayerUIManager playerUIManager;
        public PlayerInventoryManager playerInventoryManager;

        [Header("Components")]
        [SerializeField] Transform itemsParent;

        [Header("Slot Handling")]
        private InventorySlot[] slots;

        private void Awake()
        {
            playerInventoryManager = PlayerManager.instance.playerInventoryManager;
        }

        private void Start()
        {
            playerUIManager = PlayerUIManager.instance;
            playerInventoryManager = PlayerManager.instance.playerInventoryManager;

            playerInventoryManager.onItemChangedCallback += UpdateUI;

            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        }

        private void UpdateUI()
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if(i < playerInventoryManager.items.Count)
                {
                    slots[i].AddItem(playerInventoryManager.items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }
}

