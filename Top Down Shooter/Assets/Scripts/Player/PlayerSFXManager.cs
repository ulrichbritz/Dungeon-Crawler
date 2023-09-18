using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class PlayerSFXManager : CharacterSoundFXManager
    {
        //Player Only Actions
        [SerializeField] private AudioClip loot;

        [Header("Abilities")]
        [SerializeField] private AudioClip ability1_Part_1;
        [SerializeField] private AudioClip ability1;
        [SerializeField] private AudioClip ability2_Part_1;
        [SerializeField] private AudioClip ability2;
        [SerializeField] private AudioClip ability3_Part_1;
        [SerializeField] private AudioClip ability3;
        [SerializeField] private AudioClip ability4_Part_1;
        [SerializeField] private AudioClip ability4;

        public void PlayLootSFX(float volume = 1)
        {
            actionAudioSource.volume = volume;
            actionAudioSource.PlayOneShot(loot);
        }

        public void PlayAbility1Part1(float volume)
        {
            actionAudioSource.volume = volume;
            actionAudioSource.PlayOneShot(ability1_Part_1);
        }

        public void PlayAbility1SFX(float volume = 1f)
        {
            actionAudioSource.volume = volume;
            actionAudioSource.PlayOneShot(ability1);
        }

        public void PlayAbility2Part1(float volume)
        {
            actionAudioSource.volume = volume;
            actionAudioSource.PlayOneShot(ability2_Part_1);
        }

        public void PlayAbility2SFX(float volume = 1f)
        {
            actionAudioSource.volume = volume;
            actionAudioSource.PlayOneShot(ability2);
        }

        public void PlayAbility3Part1(float volume)
        {
            actionAudioSource.volume = volume;
            actionAudioSource.PlayOneShot(ability3_Part_1);
        }

        public void PlayAbility3SFX(float volume = 1f)
        {
            actionAudioSource.volume = volume;
            actionAudioSource.PlayOneShot(ability3);
        }

        public void PlayAbility4Part1(float volume)
        {
            actionAudioSource.volume = volume;
            actionAudioSource.PlayOneShot(ability4_Part_1);
        }

        public void PlayAbility4SFX(float volume = 1f)
        {
            actionAudioSource.volume = volume;
            actionAudioSource.PlayOneShot(ability4);
        }
    }
}

