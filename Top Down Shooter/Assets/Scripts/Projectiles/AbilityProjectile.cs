using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class AbilityProjectile : MonoBehaviour
    {
        private Rigidbody rb;

        [HideInInspector] public int carriedPhysicalDamage;
        [HideInInspector] public int carriedMagicalDamage;
        [HideInInspector] public Vector3 instantiationPoint;
        [HideInInspector] public float travelDistance;
        Vector3 startingPos;

        [SerializeField] private float projectileSpeed;

        [SerializeField] private DamageCollider damageCollider;

        public Quaternion shootRotation;

        private Vector3 position;
        private RaycastHit hit;
        private Ray ray;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            damageCollider.OnEnable();
            startingPos = transform.position;
        }

        private void FixedUpdate()
        {
            transform.rotation = shootRotation;

            gameObject.transform.TransformDirection(Vector3.forward);
            gameObject.transform.Translate(new Vector3(0, 0, projectileSpeed * Time.deltaTime));

            if(Vector3.Distance(transform.position, startingPos) >= travelDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}

