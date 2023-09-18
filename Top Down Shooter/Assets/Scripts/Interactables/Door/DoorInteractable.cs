using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class DoorInteractable : Interactable
    {
        [SerializeField] private bool isSafeRoomDoor = false;

        [SerializeField] GameObject lightObject;

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

            print("Interacted with door");

            if (!hasInteracted)
            {
                hasInteracted = true;

                if (!isSafeRoomDoor)
                {
                    StartCoroutine(MoveToSafeRoom());
                }
                else
                {
                    StartCoroutine(MoveToNextRoom(3));
                }    
            }

        }

        IEnumerator MoveToNextRoom(int sceneIndex)
        {
            lightObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            hasInteracted = false;

            WorldSceneManager.instance.LoadScene(WorldSceneManager.instance.safeSpaceRoomIndex + 1);
        }

        IEnumerator MoveToSafeRoom()
        {
            lightObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            hasInteracted = false;
            lightObject.SetActive(false);

            WorldSceneManager.instance.LoadScene(WorldSceneManager.instance.safeSpaceRoomIndex);
        }
    }
}

