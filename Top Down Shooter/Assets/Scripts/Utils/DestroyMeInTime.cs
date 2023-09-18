using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class DestroyMeInTime : MonoBehaviour
    {
        [SerializeField] private float seconds;

        private void Awake()
        {
            StartCoroutine(DestroyMeInSeconds());
        }

        private IEnumerator DestroyMeInSeconds()
        {
            yield return new WaitForSeconds(seconds);

            Destroy(gameObject);
        }
    }
}

