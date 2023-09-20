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

        protected override void Update()
        {
            base.Update();

            if (isFocus)
            {
                float distance = Vector3.Distance(playerTransform.position, interactionTransform.position);
                if (distance <= raduis)
                {
                    Interact();
                }
            }
        }

        public override void Interact()
        {
            print("Start interact with merchant");

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

