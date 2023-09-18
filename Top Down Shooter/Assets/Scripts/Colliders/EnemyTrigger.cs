using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class EnemyTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject enemies;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                enemies.gameObject.SetActive(true);

                Destroy(gameObject);
            }
        }
    }
}

