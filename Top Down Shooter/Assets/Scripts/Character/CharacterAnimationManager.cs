using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UB
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        [Header("Scripts")]
        private CharacterManager characterManager;

        [Header("Attributes")]
        const float locomotionAnimationSmoothTime = .1f;
        protected float speedPercent;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Update()
        {
            speedPercent = characterManager.navMeshAgent.velocity.magnitude / characterManager.navMeshAgent.speed;
            characterManager.animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

            

        }

        public virtual void PlayTargetAnimation(string animation, bool isPerformingAction, bool applyRootMotion = false)
        {
            characterManager.isPerformingAction = isPerformingAction;

            characterManager.animator.applyRootMotion = applyRootMotion;

            characterManager.animator.CrossFade(animation, 0.1f);
        }
    }
}

