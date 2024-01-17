using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float crouchSpeed = 2.5f;
    [SerializeField] float rotationSpeed = 5f;
    Vector3 playerScale = new Vector3(1, 1, 1);
    Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    Vector2 moveDirection = Vector2.zero;
    CharacterController characterController;
    bool isSprinting = false;
    bool isCrouching = false;
    public bool isBlocking = true;

    PlayerInput playerControls;
    InputAction move;
    InputAction sprint;
    InputAction crouch;
    InputAction fire;



    void Awake() {
        playerControls = new PlayerInput();
        characterController = GetComponent<CharacterController>();
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
        fire.performed += Fire;
    }

    void OnDisable() {
        move.Disable();
        fire.Disable();
    }

    void Update() {
        handleMovement();
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

            Vector3 newPosition = transform.position +
                new Vector3(moveDirection.x, 0, moveDirection.y) * currentSpeed * Time.deltaTime;

        //transform.position = newPosition;
        characterController.SimpleMove((newPosition - transform.position) / Time.deltaTime);

        if (moveDirection != Vector2.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

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

    void Fire(InputAction.CallbackContext context) {
        Debug.Log("Attacked");
    }

}
