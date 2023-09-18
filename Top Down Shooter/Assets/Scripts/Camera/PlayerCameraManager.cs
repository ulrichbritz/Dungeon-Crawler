using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class PlayerCameraManager : MonoBehaviour
    {
        public static PlayerCameraManager instance;

        [Header("Scripts")]
        [SerializeField] public PlayerManager playerManager;

        [Header("Camera Components")]
        public Camera cameraObject;

        [Header("Camera Follow")]
        private Transform playerTransform;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float pitch = 2f;  //player character height

        [Header("Camera Zoom")]
        private float currentZoom = 10f;
        [SerializeField] private float zoomSpeed = 4f;
        [SerializeField] private float minZoom = 5f;
        [SerializeField] private float maxZoom = 15f;

        [Header("Camera Movement")]
        private bool isManualMove = false;
        [SerializeField] private float yawSpeed = 100f;
        private float currentYaw = 0f;
        [SerializeField] private float cameraMoveSpeed = 10f;
        private Vector3 startCamPos;
        [SerializeField] private float rotationSpeed = 100f;

        [Header("Obstacle Detection")]
        [SerializeField] private LayerMask obstacleMask;
        Vector2 midScreen = new Vector2(0,0);
        private List<GameObject> obstaclesInWayList = new List<GameObject>();

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

        private void Start()
        {
            playerManager = PlayerManager.instance;
            playerTransform = playerManager.transform;

            float screenX = (Screen.width / 2);
            float screenY = (Screen.height / 2);

            midScreen = new Vector2(screenX, screenY);
        }

        private void Update()
        {
            HandleZoom();
            //CalculateCurrentYawForRotation();
            HandleCameraMovement();
            //CheckForWall();
            //HandleCameraRotation();
        }

        private void LateUpdate()
        {
            FollowPlayer();
            
        }

        #region Update methods
        private void HandleZoom()
        {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }

        private void CalculateCurrentYawForRotation()
        {
            currentYaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        }

        private void HandleCameraMovement()
        {
            Vector3 inputDirection = new Vector3(0, 0, 0);

            if (Input.GetKey(KeyCode.W))
            {
                isManualMove = true;
                inputDirection.z += 1f;
            }

            if (Input.GetKey(KeyCode.S))
            {
                isManualMove = true;
                inputDirection.z -= 1f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                isManualMove = true;
                inputDirection.x += -1f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                isManualMove = true;
                inputDirection.x += 1f;
            }

            Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
            transform.position += moveDirection * cameraMoveSpeed * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isManualMove = false;
            }
        }

        private void CheckForWall()
        {
            Ray ray = PlayerCameraManager.instance.cameraObject.ScreenPointToRay(midScreen);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, obstacleMask))
            {
                GameObject obstacle = null;

                //check to see if ray hit obstacle
                if (hit.collider != null)
                {
                    print("Hit wall");
                    obstacle = hit.collider.gameObject;

                    if(obstacle.transform.position.z <= playerTransform.position.z)
                    {
                        /*
                    obstacle.GetComponent<MeshRenderer>().enabled = false;

                    if (!obstaclesInWayList.Contains(obstacle))
                    {
                        obstaclesInWayList.Add(obstacle);
                    } 
                    */
                    }
                }

                /*
                foreach (GameObject obstacleInList in obstaclesInWayList)
                {
                    if (!obstacleInList == obstacle)
                    {
                        obstacleInList.GetComponent<MeshRenderer>().enabled = true;
                        obstaclesInWayList.Remove(obstacleInList);
                    }

                }
                */

            }
        }

        private void HandleCameraRotation()
        {
            float rotateDirection = 0f;

            if (Input.GetKey(KeyCode.Q))
            {
                rotateDirection += 1f;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                rotateDirection += -1f;
            }

            float angle = rotateDirection * rotationSpeed * Time.deltaTime;

            transform.RotateAround(playerTransform.position, Vector3.up, angle);
        }

        #endregion

        #region Late update methods
        private void FollowPlayer()
        {
            if(!isManualMove)
                transform.position = playerTransform.position - offset * currentZoom;
            //transform.LookAt(playerTransform.position + Vector3.up * pitch);
        }

     
        #endregion
    }
}

