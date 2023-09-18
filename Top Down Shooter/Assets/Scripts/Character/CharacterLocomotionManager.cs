using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UB
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        protected CharacterManager characterManager;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            characterManager.navMeshAgent.speed = characterManager.GetComponent<CharacterStats>().GetMoveSpeed();
        }

        protected virtual void Update()
        {

        }
    }
}

