using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{

    //[SerializeField] Canvas gameOverCanvas;
    PlayerMovement playerControls;
    Animator animator;



    void Start() {

        playerControls = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        playerControls.enabled = true;
        //gameOverCanvas.enabled = false;

    }

    public void HandleDeath() {

        playerControls.enabled = false;
        animator.enabled = false;
        //gameOverCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
