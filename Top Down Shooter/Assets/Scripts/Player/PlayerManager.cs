using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UB
{
    public class PlayerManager : CharacterManager
    {
        public static PlayerManager instance;

        [SerializeField] public int characterID;

        [Header("Scripts")]
        [HideInInspector] public PlayerInputManager playerInputManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerAnimationManager playerAnimationManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerStats playerStats;
        [HideInInspector] public HighlightManager highlightManager;
        [HideInInspector] public PlayerSFXManager playerSFXManager;
        [HideInInspector] public Abilities abilities;

        [Header("LeftClick")]
        public LayerMask movementMask;

        [Header("RightClick")]
        public LayerMask interactableMask;

        [Header("Interaction")]
        [HideInInspector] public Interactable focus;

        [Header("Player Specific Flags")]
        [HideInInspector] public bool hasUIOpen = false;

        protected override void Awake()
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

            base.Awake();

            playerInputManager = GetComponent<PlayerInputManager>();
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimationManager = GetComponent<PlayerAnimationManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            highlightManager = GetComponent<HighlightManager>();
            playerSFXManager = GetComponent<PlayerSFXManager>();
            abilities = GetComponent<Abilities>();
            playerStats = GetComponent<PlayerStats>();
        }

       
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            
        }

        public void HandleLeftClick()
        {
            if (abilities.CheckForAbilityInput() == true)
                return;

            //shoot ray from camera to mouse pos
            Ray ray = PlayerCameraManager.instance.cameraObject.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //move player to what we hit
                playerLocomotionManager.MoveToPoint(hit.point);

                //stop focusing any objects
                RemoveFocus();
            }
        }

        public void HandleRightClick()
        {
            //shoot ray from camera to mouse pos
            Ray ray = PlayerCameraManager.instance.cameraObject.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableMask))
            {
                //check if hit an interactable, and set if focus if true
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        private void SetFocus(Interactable newFocus)
        {
            if(newFocus != focus)
            {
                if (focus != null)
                    focus.OnDefocused();

                focus = newFocus;
                playerLocomotionManager.FollowTarget(newFocus);
            }
      
            newFocus.OnFocused(transform);           
        }

        public void RemoveFocus()
        {
            if (focus != null)
            {
                highlightManager.DeselectHighlight();
                focus.OnDefocused();
            }
                

            focus = null;
            playerLocomotionManager.StopFollowingTarget();
        }
    }
}

