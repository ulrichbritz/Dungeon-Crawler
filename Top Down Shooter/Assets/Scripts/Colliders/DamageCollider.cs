using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class DamageCollider : MonoBehaviour
    {
        [SerializeField] protected Collider collider;
        [SerializeField] protected GameObject bloodPrefab;

        public Transform target;

        protected virtual void Awake()
        {
            if(collider == null)
                collider = GetComponent<Collider>();
        }

        public virtual void OnEnable()
        {
            collider.enabled = true;
        }

        public virtual void OnDisable()
        {
            collider.enabled = false;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            
        }

    }
}

