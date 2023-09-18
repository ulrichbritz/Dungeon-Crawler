using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class Interactable : MonoBehaviour
    {
        protected bool isFocus = false;
        protected Transform playerTransform;

        protected bool hasInteracted = false;

        [Header("Attibutes")]
        public float raduis = 3f;
        public Transform interactionTransform;

        protected virtual void Awake()
        {
            if(interactionTransform == null)
            {
                interactionTransform = transform;
            }
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            
        }

        public virtual void Interact()
        {

        }

        public void OnFocused(Transform playerTransform)
        {
            isFocus = true;
            this.playerTransform = playerTransform;
            hasInteracted = false;
            if(interactionTransform == null)
            {
                interactionTransform = transform;
            }
        }

        public void OnDefocused()
        {
            isFocus = false;
            playerTransform = null;
            hasInteracted = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(interactionTransform.position, raduis);
        }
    }
}

