using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class Billboard : MonoBehaviour
    {
        PlayerCameraManager playerCameraManager;
        Vector3 cameraDirection;
        [SerializeField] GameObject canvasInUse;


        private void Update()
        {
            if (playerCameraManager != null)
            {
                cameraDirection = playerCameraManager.cameraObject.transform.forward;
                cameraDirection.y = 0;

                canvasInUse.transform.rotation = Quaternion.LookRotation(cameraDirection);
            }
            else
            {
                playerCameraManager = PlayerCameraManager.instance;
            }
        }


    }
}

