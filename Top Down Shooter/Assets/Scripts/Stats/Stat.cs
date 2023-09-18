using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    [System.Serializable]
    public class Stat 
    {
        [SerializeField] private int baseValue = 1;

        private List<int> modifiers = new List<int>();

        public float GetValue()
        {
            return baseValue;
        }

        public void AddModifier(int _modifier)
        {
            if(_modifier != 0)
            {
                modifiers.Add(_modifier);
            }
        }

        public void RemoveModifier(int _modifier)
        {
            if(_modifier != 0)
            {
                modifiers.Remove(_modifier);
            }
        }
    }
}

