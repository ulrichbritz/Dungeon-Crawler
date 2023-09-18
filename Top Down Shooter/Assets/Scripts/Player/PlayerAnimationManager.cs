using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class PlayerAnimationManager : CharacterAnimationManager
    {
        PlayerManager playerManager;

        protected override void Awake()
        {
            base.Awake();

            playerManager = GetComponent<PlayerManager>();
        }

        protected override void Update()
        {
            base.Update();

            
            if (speedPercent > 0.2f && !playerManager.isPerformingAction)
            {
                playerManager.playerSFXManager.footStepsAudioSource.volume = 1f;
            }
            else
            {
                playerManager.playerSFXManager.footStepsAudioSource.volume = 0f;
            }
            
        }
    }
}

