using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace UB
{
    public class MobSpawner : MonoBehaviour
    {
        public static MobSpawner instance;

        [Header("Spawning")]
        [SerializeField] List<GameObject> enemyTypes;
        [SerializeField] int enemyAmountToSpawn;
        [SerializeField] GameObject spawnHolder;

        [SerializeField] Vector2 xRange;
        [SerializeField] int yRange;
        [SerializeField] Vector2 zRange;

        [SerializeField] LayerMask navmeshLayer;

        Vector3 checkResult = Vector3.zero;

        [Header("Game Loop Checks")]
        [HideInInspector] public List<EnemyManager> enemiesAlive = new List<EnemyManager>();
        [SerializeField] private GameObject doorToSafeRoom;
        [SerializeField] private TextMeshProUGUI enemiesLeftText;
        [SerializeField] private GameObject dungeonClearedObj;
        [SerializeField] private TextMeshProUGUI dungeonClearedBackgroundText;
        [SerializeField] private TextMeshProUGUI dungeonClearedText;
        [SerializeField] CanvasGroup dungeonClearedCanvasGroup;

        [Header("Path")]
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] float pathHeightOffset = 1.25f;
        [SerializeField] float pathUpdateSpeed = 1f;
        private NavMeshTriangulation triangulation;


        //x = 50 to -100
        //y = 0
        //z = -50 to 100

        private void Awake()
        {
            instance = this;

            triangulation = NavMesh.CalculateTriangulation();

            enemiesLeftText.text = "Enemies Left: ";
            dungeonClearedObj.SetActive(false);

            // SpawnMobs();
            GetEnemiesAliveAtStart();
        }

        private void SpawnMobs()
        {
            for (int i = 0; i < enemyAmountToSpawn; i++)
            {
                var enemyTypeToSpawn = Random.Range(0, enemyTypes.Count);
                int randX = Random.Range(Mathf.FloorToInt(xRange.x), Mathf.FloorToInt(xRange.y));
                int randZ = Random.Range(Mathf.FloorToInt(zRange.x), Mathf.FloorToInt(zRange.y));
                Vector3 randPos = new Vector3(randX, yRange, randZ);

                if(pointOnNavmesh(randPos, out checkResult))
                {
                    var spawnedEnemy = Instantiate(enemyTypes[enemyTypeToSpawn], checkResult, Quaternion.identity);
                    spawnedEnemy.transform.SetParent(spawnHolder.transform);
                    EnemyManager enemyManager = spawnedEnemy.GetComponent<EnemyManager>();
                    if(enemyManager != null)
                    {
                        enemiesAlive.Add(enemyManager);
                    }
                }
                else
                {
                    i--;
                }
            }

            enemiesLeftText.text = "Enemies Left = " + enemiesAlive.Count;
        }

        public static bool pointOnNavmesh(Vector3 randomPoint, out Vector3 result)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
            else
            {
                result = Vector3.zero;
                return false;
            }
        }

        private void GetEnemiesAliveAtStart()
        {
            EnemyManager[] enemyManagers = FindObjectsOfType<EnemyManager>();

            foreach(EnemyManager enemyManager in enemyManagers)
            {
                enemiesAlive.Add(enemyManager);
            }

            enemiesLeftText.text = "Enemies Left = " + enemiesAlive.Count;
        }

        public void CheckIfWasLastEnemy(EnemyManager enemyManager)
        {
            enemiesAlive.Remove(enemyManager);

            enemiesLeftText.text = "Enemies Left = " + enemiesAlive.Count;

            if (enemiesAlive.Count <= 0)
            {
                SpawnDoor();
                enemiesLeftText.text = "Find the exit.";
            }
        }

        private void SpawnDoor()
        {
            doorToSafeRoom.SetActive(true);

            ShowPathToGoal();
        }

        private void ShowPathToGoal()
        {
            dungeonClearedObj.SetActive(true);
            dungeonClearedBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopUpTextOverTime(dungeonClearedBackgroundText, 8f, 15f));
            StartCoroutine(FadeInTextOverTime(dungeonClearedCanvasGroup, 5f));
            StartCoroutine(WaitThenFadeOutTextOverTime(dungeonClearedCanvasGroup, 2, 5f));

            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(PlayerManager.instance.transform.position, doorToSafeRoom.transform.position, NavMesh.AllAreas, path); //Saves the path in the path variable.
            Vector3[] corners = path.corners;
            lineRenderer.SetPositions(corners);
        }

        private IEnumerator StretchPopUpTextOverTime(TextMeshProUGUI text, float duration, float strechAmount)
        {
            if(duration > 0f)
            {
                text.characterSpacing = 0;
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer = timer + Time.deltaTime;
                    text.characterSpacing = Mathf.Lerp(text.characterSpacing, strechAmount, duration * (Time.deltaTime / 20f));
                }
            }
        }

        private IEnumerator FadeInTextOverTime(CanvasGroup canvas, float duration)
        {
            if(duration > 0)
            {
                canvas.alpha = 0;
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration * Time.deltaTime);
                }

                canvas.alpha = 1f;

                yield return null;
            }
        }

        private IEnumerator WaitThenFadeOutTextOverTime(CanvasGroup canvas, float duration, float delay)
        {
            if (duration > 0)
            {
                while(delay > 0)
                {
                    delay = delay - Time.deltaTime;
                    yield return null;
                }

                canvas.alpha = 1;
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * Time.deltaTime);
                }

                canvas.alpha = 0f;

                yield return null;
            }
        }

        
    }
}

