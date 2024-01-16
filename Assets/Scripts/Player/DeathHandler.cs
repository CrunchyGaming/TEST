using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{

    //[SerializeField] Canvas gameOverCanvas;
    PlayerMovement playerControls;



    void Start() {

        playerControls = GetComponent<PlayerMovement>();
        playerControls.enabled = true;
        //gameOverCanvas.enabled = false;

    }

    public void HandleDeath() {

        playerControls.enabled = false;
        //gameOverCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
