using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UB
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterManager : MonoBehaviour
    {
        [Header("Scripts")]
        public CharacterAnimationManager characterAnimationManager;

        [Header("Components")]
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Animator animator;

        [Header("Flags")]
        public bool isDead = false;
        public bool isPerformingAction = false;

        protected virtual void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            if(animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
            characterAnimationManager = GetComponent<CharacterAnimationManager>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }
    }
}

