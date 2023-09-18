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

        
    }
}

