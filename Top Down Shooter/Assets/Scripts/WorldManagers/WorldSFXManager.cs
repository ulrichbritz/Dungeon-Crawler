using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class WorldSFXManager : MonoBehaviour
    {
        public static WorldSFXManager instance;

        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource ambienceAudioSource;
        [SerializeField] private AudioSource uiAudioSource;

        [Header("General UI")]
        [SerializeField] private AudioClip gameErrorSFX;

        [Header("Shop SFX")]
        [SerializeField] private AudioClip successfullItemPurchaseSFX;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        #region UI SFX
        //GENERAL UI
        public void PlayGameErrorSFX(float volume)
        {
            uiAudioSource.volume = volume;
            uiAudioSource.PlayOneShot(gameErrorSFX);
        }

        //SHOP
        public void PlaySuccessFulItemPurchase(float volume)
        {
            uiAudioSource.volume = volume;
            uiAudioSource.PlayOneShot(successfullItemPurchaseSFX);
        }

        #endregion


    }
}

