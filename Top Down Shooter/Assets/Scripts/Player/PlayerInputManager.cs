using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UB
{
    public class PlayerInputManager : MonoBehaviour
    {
        PlayerControls playerControls;

        [Header("Scripts")]
        PlayerManager playerManager;

        [Header("Movement")]
        private bool leftClickInput = false;

        [Header("Actions")]
        private bool rightClickInput = false;
        private bool ability1Input = false;
        private bool ability2Input = false;
        private bool ability3Input = false;
        private bool ability4Input = false;

        [Header("Camera")]
        [HideInInspector] public Vector2 cameraScrollAmount;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        private void OnEnable()
        {
            if(playerControls == null)
            {
                playerControls = new PlayerControls();

                //camera
                //playerControls.CameraActions.Zoom.performed += inputActions => cameraScrollAmount = inputActions.ReadValue<Vector2>();

                //movement
                playerControls.PlayerMovement.LeftClickMovement.performed += inputActions => leftClickInput = true;

                //actions
                playerControls.PlayerActions.RightClick.performed += inputActions => rightClickInput = true;
                playerControls.PlayerActions.Ability1.performed += inputAction => ability1Input = true;
                playerControls.PlayerActions.Ability2.performed += inputAction => ability2Input = true;
                playerControls.PlayerActions.Ability3.performed += inputAction => ability3Input = true;
                playerControls.PlayerActions.Ability4.performed += inputAction => ability4Input = true;
            }

            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update()
        {
            HandleAllInputs();
        }
        private void HandleAllInputs()
        {
            //movement
            HandleLeftClickInput();

            //actions
            HandleRightClickInput();
            HandleAbility1Input();
            HandleAbility2Input();
            HandleAbility3Input();
            HandleAbility4Input();
        }

        #region Movement
        private void HandleLeftClickInput()
        {
            if (leftClickInput)
            {
                leftClickInput = false;

                //check to see if pointer over ui
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                playerManager.HandleLeftClick(); 
            }
        }

        #endregion

        #region Actions
        private void HandleRightClickInput()
        {
            if (rightClickInput)
            {
                rightClickInput = false;

                //check to see if pointer over ui
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                playerManager.HandleRightClick();
            }
        }

        private void HandleAbility1Input()
        {
            if (ability1Input)
            {
                ability1Input = false;

                playerManager.abilities.Ability1Input();
            }
        }

        private void HandleAbility2Input()
        {
            if (ability2Input)
            {
                ability2Input = false;

                playerManager.abilities.Ability2Input();
            }
        }

        private void HandleAbility3Input()
        {
            if (ability3Input)
            {
                ability3Input = false;

                playerManager.abilities.Ability3Input();
            }
        }

        private void HandleAbility4Input()
        {
            if (ability4Input)
            {
                ability4Input = false;

                playerManager.abilities.Ability4Input();
            }
        }

        #endregion
    }
}

