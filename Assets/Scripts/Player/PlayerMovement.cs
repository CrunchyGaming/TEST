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
    [SerializeField] float rotationSpeed = 5f;
    Vector2 moveDirection = Vector2.zero;
    bool isSprinting = false;

    PlayerInput playerControls;
    InputAction move;
    InputAction sprint;
    InputAction fire;



    void Awake() {
        playerControls = new PlayerInput();
    }

    void OnEnable() {
        move = playerControls.Player.Move;
        move.Enable();

        sprint = playerControls.Player.Sprint;
        sprint.Enable();
        sprint.performed += SprintStart;
        sprint.canceled += SprintEnd;

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

        Vector3 newPosition = transform.position +
            new Vector3(moveDirection.x, 0, moveDirection.y) * currentSpeed * Time.deltaTime;

        transform.position = newPosition;

        if (moveDirection != Vector2.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        bool isMoving = moveDirection.magnitude > 0.1f;

    }

    void SprintStart(InputAction.CallbackContext context) {
        isSprinting = true;
    }

    void SprintEnd(InputAction.CallbackContext context) {
        isSprinting = false;
    }

    void Fire(InputAction.CallbackContext context) {
        Debug.Log("Attacked");
    }

}
