using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        [Header("Scripts")]
        PlayerManager playerManager;

        [Header("Inventory Values")]
        public int capacity = 6;
        [HideInInspector] public int goldAmount = 0;

        public List<Item> items = new List<Item>();

        //CallBacks
        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;
        public delegate void OnGoldChanged();
        public OnGoldChanged OnGoldChangedCallback;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        public bool Add(Item item)
        {
            if (!item.isDefaultItem)
            {
                //check if space in inventory, if not, return
                if(items.Count >= capacity)
                {
                    Debug.Log("Not enough room in inventory");
                    return false;
                }

                items.Add(item);

                if(onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }

            return true;
        }

        public void Remove(Item item)
        {
            items.Remove(item);

            Instantiate(item.itemPrefab, transform.position, Quaternion.identity);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        public void GetGold(int amount)
        {
            goldAmount += amount;

            if (OnGoldChangedCallback != null)
                OnGoldChangedCallback.Invoke();
        }

        public void LoseGold(int amount)
        {
            goldAmount -= amount;

            if (OnGoldChangedCallback != null)
                OnGoldChangedCallback.Invoke();
        }
    }
}

