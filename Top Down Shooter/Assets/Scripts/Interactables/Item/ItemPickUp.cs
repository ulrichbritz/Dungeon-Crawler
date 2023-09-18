using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class ItemPickUp : Interactable
    {
        public Item item;
        private bool hasBeenLooted = false;

        protected override void Update()
        {
            base.Update();

            if (isFocus && !hasInteracted)
            {
                float distance = Vector3.Distance(playerTransform.position, interactionTransform.position);
                if (distance <= raduis)
                {
                    Interact();
                    hasInteracted = true;
                }
            }
        }

        public override void Interact()
        {
            base.Interact();

            PlayerAnimationManager playerAnimationManager = playerTransform.GetComponent<PlayerAnimationManager>();
            if(playerAnimationManager != null)
            {
                playerAnimationManager.PlayTargetAnimation("Pick_Up_01", true, false);
            }

            PickUp();
        }

        void PickUp()
        {
            if (!hasBeenLooted)
            {
                Debug.Log("Picking up " + item.name);

                //add item to inventory
                bool wasPickedUp = playerTransform.GetComponent<PlayerManager>().playerInventoryManager.Add(item);

                if (wasPickedUp)
                {
                    hasBeenLooted = true;
                    StartCoroutine(DestroyMe());
                }
                    
            }   
        }

        IEnumerator DestroyMe()
        {
            yield return new WaitForSeconds(0.5f);

            Destroy(gameObject);
        }
    }
}

