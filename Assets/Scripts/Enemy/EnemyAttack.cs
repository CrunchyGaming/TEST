using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] float damageAmount = 40f;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    bool isBlocking = false;



    void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update() {
        isBlocking = playerMovement.isBlocking;    
    }

    //in attack animation (activates this)
    public void AttackHitEvent() {
        if (playerHealth == null) return;

        if (isBlocking == true) {
            return;
        } else if (isBlocking == false) {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Do Damage");
        } else {
            return;
        }

    }

}
