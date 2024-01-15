using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    Vector2 moveDirection = Vector2.zero;

    [SerializeField] PlayerInput playerControls;
    InputAction move;
    InputAction fire;



    void Awake() {
        playerControls = new PlayerInput();
    }

    void OnEnable() {
        move = playerControls.Player.Move;
        move.Enable();

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

        Vector3 newPosition = transform.position +
            new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed * Time.deltaTime;

        transform.position = newPosition;

        if (moveDirection != Vector2.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Fire(InputAction.CallbackContext context) {
        Debug.Log("Fired");
    }

}
