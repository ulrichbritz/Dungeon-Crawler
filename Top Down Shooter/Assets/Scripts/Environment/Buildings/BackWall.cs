using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class BackWall : MonoBehaviour
    {
        Transform playerTransform;

        [SerializeField] GameObject normalBackWall;
        [SerializeField] GameObject shrunkenBackWall;

        private bool isShrunk = false;

        private void Awake()
        {
            normalBackWall.SetActive(true);
            shrunkenBackWall.SetActive(false);
        }

        private void Start()
        {
            playerTransform = PlayerManager.instance.transform;
        }

        private void Update()
        {
            if(playerTransform != null)
            {
                // print("player pos = " + playerTransform.position.z + " wall pos = " + transform);
                if (transform.position.z <= playerTransform.position.z && isShrunk == false)
                {
                    Shrink();
                }
                else if (transform.position.z > playerTransform.position.z && isShrunk == true)
                {
                    Grow();
                }
            }
           
        }

        public void Shrink()
        {
            normalBackWall.SetActive(false);
            shrunkenBackWall.SetActive(true);
            isShrunk = true;
        }

        public void Grow()
        {
            shrunkenBackWall.SetActive(false);
            normalBackWall.SetActive(true);
            isShrunk = false;
        }


    }
}

