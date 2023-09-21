using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UB
{
    public class EnemyManager : CharacterManager
    {
        [Header("Scripts")]
        private CharacterManager characterManager;
        private PlayerManagingScript playerManagingScript;
        private EnemyStats enemyStats;
        private EnemyCombatManager enemyCombatManager;

        [Header("Aggro")]
        [SerializeField] private float lookRaduis = 20f;
        Transform target;
        private bool isWaiting = false;

        [Header("Attack")]
        [SerializeField] private float attackDistance;

        protected override void Awake()
        {
            base.Awake();

            characterManager = GetComponent<CharacterManager>();
            enemyStats = GetComponent<EnemyStats>();
            enemyCombatManager = GetComponent<EnemyCombatManager>();

            navMeshAgent.speed = enemyStats.moveSpeed;
            attackDistance = enemyStats.GetAttackRange();
        }

        protected override void Start()
        {
            base.Start();

            playerManagingScript = PlayerManagingScript.instance;
        }

        private void Update()
        {
            if (!isPerformingAction)
            {
                navMeshAgent.isStopped = false;
                float distance = Vector3.Distance(playerManagingScript.player.transform.position, transform.position);

                if (distance <= lookRaduis)
                {
                    navMeshAgent.SetDestination(playerManagingScript.player.transform.position);

                    if (distance <= attackDistance)
                    {
                        //face target
                        FaceTarget();

                        //attack player
                       // characterAnimationManager.PlayTargetAnimation("Basic_Attack_01", true, false);
                        CharacterStats targetStats = playerManagingScript.player.GetComponent<CharacterStats>();

                        if (targetStats != null)
                            enemyCombatManager.Attack(targetStats);

                    }
                }
                else
                {
                    if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) //done with path
                    {
                        if(isWaiting == false)
                        {
                            StartCoroutine(GetNewRoamPoint());
                        }  
                    }
                }
            }
            else
            {
                navMeshAgent.isStopped = true;
                FaceTarget();
            }        

        }

        private bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;

            if(NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }

        private IEnumerator GetNewRoamPoint()
        {
            isWaiting = true;

            float randWaitTime = Random.Range(0, 5f);
            yield return new WaitForSeconds(randWaitTime);

            Vector3 point;
            if (RandomPoint(transform.position, lookRaduis, out point))
            {
                navMeshAgent.SetDestination(point);
            }

            isWaiting = false;
        }


        private void FaceTarget()
        {
            Vector3 direction = (playerManagingScript.player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRaduis);
        }
    }
}

