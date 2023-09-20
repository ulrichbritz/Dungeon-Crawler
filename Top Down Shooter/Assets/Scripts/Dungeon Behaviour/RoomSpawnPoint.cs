using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class RoomSpawnPoint : MonoBehaviour
    {
        private void Awake()
        {
            PlayerManager.instance.transform.position = transform.position;
            PlayerManager.instance.navMeshAgent.enabled = true;

            PlayerManager.instance.playerAnimationManager.PlayTargetAnimation("Enter_01", true, false);
        }
    }
}

