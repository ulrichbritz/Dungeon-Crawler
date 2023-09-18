using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        protected AudioSource actionAudioSource;
        public AudioSource footStepsAudioSource;

        //Attacks
        [SerializeField] private AudioClip basicAttackAudioClip;

        //Movement
        [SerializeField] private AudioClip walkingAudioClip;

        //Reactions
        [SerializeField] private AudioClip deathAudioClip;
        [SerializeField] private AudioClip hurtAudioClip;


        protected virtual void Awake()
        {
            actionAudioSource = GetComponent<AudioSource>();
            if(footStepsAudioSource == null)
                footStepsAudioSource = GetComponentInChildren<AudioSource>();

            
        }

        //Attacks
        public virtual void PlayBasicAttackSFX(float volume = 1)
        {
            actionAudioSource.volume = 1f;
            actionAudioSource.PlayOneShot(basicAttackAudioClip);
        }

        //Actions


        //Reactions
        public virtual void PlayDeathSFX(float volume = 1)
        {
            actionAudioSource.volume = 1f;
            actionAudioSource.PlayOneShot(deathAudioClip);
        }

        public virtual void PlayHurtSFX(float volume = 1)
        {
            actionAudioSource.volume = 1f;
            actionAudioSource.PlayOneShot(hurtAudioClip);
        }

    }
}

