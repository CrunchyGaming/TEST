using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float crouchSpeed = 2.5f;
    [SerializeField] float blockSpeed = 2.5f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] LayerMask targetLayer;
    Vector3 playerScale = new Vector3(1, 1, 1);
    Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    Vector2 moveDirection = Vector2.zero;
    CharacterController characterController;
    HandleRanged handleRanged;
    EnemyHealth enemyHealth;
    Animator animator;
    bool isSprinting = false;
    bool isCrouching = false;
    public bool isLooking = false;
    public bool isAttacking { get; private set; }
    public bool isBlocking { get; private set; }
    public bool isAttackButtonOn;
    public GameObject attackUISelector;

    PlayerInput playerControls;
    InputAction move;
    InputAction sprint;
    InputAction crouch;
    InputAction fire;
    InputAction block;
    InputAction look;



    void Awake() {
        playerControls = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        handleRanged = GetComponent<HandleRanged>();
        animator = GetComponent<Animator>();
        isBlocking = false;
        isAttackButtonOn = false;
    }

    void OnEnable() {
        move = playerControls.Player.Move;
        move.Enable();

        sprint = playerControls.Player.Sprint;
        sprint.Enable();
        sprint.performed += SprintStart;
        sprint.canceled += SprintEnd;

        crouch = playerControls.Player.Crouch;
        crouch.Enable();
        crouch.performed += CrouchStart;
        crouch.canceled += CrouchEnd;

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += FireStart;
        fire.canceled += FireEnd;

        block = playerControls.Player.Block;
        block.Enable();
        block.performed += BlockStart;
        block.canceled += BlockEnd;

        look = playerControls.Player.Look;
        look.Enable();
        look.performed += LookStart;
        look.canceled += LookEnd;
    }

    void OnDisable() {
        move.Disable();
        sprint.Disable();
        crouch.Disable();
        fire.Disable();
        block.Disable();
    }

    void Update() {
        handleMovement();
        ProcessHotBar();
    }

    void handleMovement() {
        moveDirection = move.ReadValue<Vector2>();

        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        if (isCrouching) {
            transform.localScale = crouchScale;
            currentSpeed = crouchSpeed;

        } else {
            transform.localScale = playerScale;
        }

        if (isBlocking) {
            currentSpeed = blockSpeed;
        }

        Vector3 newPosition = transform.position +
            new Vector3(moveDirection.x, 0, moveDirection.y) * currentSpeed * Time.deltaTime;

        characterController.SimpleMove((newPosition - transform.position) / Time.deltaTime);

        if (isLooking) {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            // Perform the raycast with the specified layerMask
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, targetLayer)) {
                Vector3 mousePosition = hit.point;
                mousePosition.y = transform.position.y;

                transform.LookAt(mousePosition);
            }
        } else {
            if (moveDirection != Vector2.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        /*if (isLooking) {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                Vector3 mousePosition = hit.point;
                mousePosition.y = transform.position.y;

                transform.LookAt(mousePosition);
            }
        } else {
            if (moveDirection != Vector2.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }*/

        bool isMoving = moveDirection.magnitude > 0.1f;
    }


    //call back methods
    void SprintStart(InputAction.CallbackContext context) {
        isSprinting = true;
    }

    void SprintEnd(InputAction.CallbackContext context) {
        isSprinting = false;
    }

    void CrouchStart(InputAction.CallbackContext context) {
        isCrouching = true;
    }

    void CrouchEnd(InputAction.CallbackContext context) {
        isCrouching = false;
    }

    void FireStart(InputAction.CallbackContext context) {
        if(!isBlocking && isAttackButtonOn && isLooking)
        {
            animator.SetBool("attack", true);
            isAttacking = true;
        }
        if(isLooking && handleRanged.isPotionButtonOn)
        {
            handleRanged.DisableInd();
        }
        
    }

    void FireEnd(InputAction.CallbackContext context) {
        animator.SetBool("attack", false);
        isAttacking = false;
    }

    void BlockStart(InputAction.CallbackContext context) {
        animator.SetBool("attack", false);
        isBlocking = true;
    }

    void BlockEnd(InputAction.CallbackContext context) {
        isBlocking = false;
    }

    void LookStart(InputAction.CallbackContext context) {
        handleRanged.EnableInd();
        isLooking = true;
    }

    void LookEnd(InputAction.CallbackContext context) {
        handleRanged.DisableIndWithoutFiring();
        animator.SetBool("attack", false);
        isLooking = false;
    }

    public void ToggleAttackButton()
    {
        if(isAttackButtonOn)
        {
            isAttackButtonOn = false;
            attackUISelector.SetActive(false);
        }
        else if(!isAttackButtonOn)
        {
            isAttackButtonOn = true;
            handleRanged.DisableIndWithoutFiring();
            handleRanged.PotionButtonOff();
            attackUISelector.SetActive(true);
            handleRanged.potionUISelector.SetActive(false);
        }
    }

    public void AttackButtonOff()
    {
        isAttackButtonOn = false;
    }

    void ProcessHotBar()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleAttackButton();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            handleRanged.TogglePotionButton();
        }
        
    }



}
