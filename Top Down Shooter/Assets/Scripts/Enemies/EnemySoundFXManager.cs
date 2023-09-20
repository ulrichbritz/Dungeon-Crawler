using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class EnemySoundFXManager : CharacterSoundFXManager
    {
        [Header("Gold")]
        [SerializeField] AudioSource extaSFXAudioSource;
        [SerializeField] private AudioClip giveGoldSFX;

        public void PlayGiveGoldSFX(float volume)
        {
            extaSFXAudioSource.volume = volume;
            extaSFXAudioSource.PlayOneShot(giveGoldSFX);
        }
    }
}

