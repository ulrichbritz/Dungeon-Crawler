using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class MerchantInteractable : Interactable
    {
        private Animator animator;

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponent<Animator>();
        }

        public override void Interact()
        {
            base.Interact();

            if (hasInteracted == false)
            {
                animator.SetTrigger("Interact");

                OpenShop();
            }
        }

        private void OpenShop()
        {
            hasInteracted = true;
            PlayerUIManager.instance.OpenShop();
        }
    }
}

