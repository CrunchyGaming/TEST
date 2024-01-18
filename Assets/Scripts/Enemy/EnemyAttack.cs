using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] float damageAmount = 40f;
    PlayerHealth playerHealth;
    PlayerControls playerMovement;
    bool isBlocking = false;



    void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMovement = FindObjectOfType<PlayerControls>();
    }

    void Update() {
        isBlocking = playerMovement.isBlocking;    
    }

    //in attack animation (activates this)
    public void EnemyAttackHitEvent() {
        if (playerHealth == null) return;

        if (isBlocking == true) {
            return;
        } else if (isBlocking == false && playerHealth.isActiveAndEnabled) {
            playerHealth.TakeDamage(damageAmount);
        } else {
            return;
        }
    }

}
