using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class EnemySoundFXManager : CharacterSoundFXManager
    {
        [Header("Coins")]
        [SerializeField] AudioSource extaSFXAudioSource;
        [SerializeField] private AudioClip giveCoinsSFX;

        public void PlayGiveCoinsSFX(float volume)
        {
            extaSFXAudioSource.volume = volume;
            extaSFXAudioSource.PlayOneShot(giveCoinsSFX);
        }
    }
}

