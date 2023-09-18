using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class BasicProjectile : MonoBehaviour
    {
        private Rigidbody rb;

        [HideInInspector] public int carriedPhysicalDamage;
        [HideInInspector] public int carriedMagicalDamage;
        [HideInInspector] public Transform target;
        [SerializeField] public Transform targetFollow;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float rotateSpeed = 2000f;

        [SerializeField] private DamageCollider damageCollider;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            damageCollider.OnEnable();
        }

        private void FixedUpdate()
        {
            if(target != null)
            {
                Vector3 direction = targetFollow.position - transform.position;
                rb.velocity = direction.normalized * projectileSpeed;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void RotateProjectile()
        {
            Vector3 directionToTarget = transform.position - targetFollow.transform.position;

            var rotation = Quaternion.LookRotation(directionToTarget);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime));
        }
    }
}

