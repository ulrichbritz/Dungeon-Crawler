using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    [CreateAssetMenu(menuName = "Inventory/Item", fileName = "New Item")]
    public class Item : ScriptableObject
    {
        [Header("Item Description")]
        new public string name = "New Item";
        public Sprite icon = null;


        public bool isDefaultItem = false;

        [Header("Item Values")]
        public int physicalDamage;
        public int magicalDamage;
        public int armor;
        public int magicResistance;
        public int health;
        public int mana;
        public int attackSpeed;
        public int attackRange;
        public int moveSpeed;

        [Header("Item Prefabs")]
        public GameObject itemPrefab;
        public virtual void Use()
        {
            Debug.Log("Using " + name);
            //use the item
        }
    }
}

