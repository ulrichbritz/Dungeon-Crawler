using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        [Header("Scripts")]
        private PlayerManager playerManager;

        [Header("Following")]
        private Transform target;   //target to follow
        private bool canCheck = true;

        [Header("WalkingSFX")]
        private bool isMoving = false;

        protected override void Awake()
        {
            base.Awake();

            playerManager = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

           // if (target != null && canCheck)   //might use coroutine
             //   StartCoroutine(UpdateFollowTarget());

            if (target != null)
            {
                playerManager.navMeshAgent.SetDestination(target.position);

                FaceTarget();
            }

            if (!characterManager.isPerformingAction)
            {
                characterManager.navMeshAgent.isStopped = false;

                if (target != null)
                    FaceTarget();
            }
            else
            {
                characterManager.navMeshAgent.isStopped = true;

                if (target != null)
                    FaceTarget();
            }

            /*
            if(playerManager.navMeshAgent.velocity.magnitude >= 0.2f && !playerManager.isPerformingAction)
            {
                if(isMoving == false)
                {
                    isMoving = true;
                    playerManager.playerSFXManager.footStepsAudioSource.volume = 0.2f;
                }
                
            }
            else 
            {
                if(isMoving == true)
                {
                    isMoving = false;
                    playerManager.playerSFXManager.footStepsAudioSource.enabled = false;
                }
                
            }
            */
                



        }

        public void MoveToPoint(Vector3 point)
        {
            playerManager.navMeshAgent.SetDestination(point);
        }

        public void FollowTarget(Interactable newTarget)
        {
            playerManager.navMeshAgent.stoppingDistance = newTarget.raduis * .8f;
            playerManager.navMeshAgent.updateRotation = false;

            target = newTarget.interactionTransform;
            canCheck = true;

            playerManager.highlightManager.SelectedHighlight();
        }

        public void StopFollowingTarget()
        {
            playerManager.navMeshAgent.stoppingDistance = 0f;
            playerManager.navMeshAgent.updateRotation = true;

            target = null;
            canCheck = true;    
        }

        private void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100f);
        }

        private IEnumerator UpdateFollowTarget()
        {
            canCheck = false;
            playerManager.navMeshAgent.SetDestination(target.position);

            yield return new WaitForSeconds(0.15f);

            canCheck = true;
        }
    }
}

