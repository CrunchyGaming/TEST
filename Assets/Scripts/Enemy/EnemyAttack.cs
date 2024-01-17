using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] float damageAmount = 40f;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;



    void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    //in attack animation (activates this)
    public void AttackHitEvent() {
        if (playerHealth == null) return;

        if (playerMovement.isBlocking) {
            return;
        } else {
            playerHealth.TakeDamage(damageAmount);
        }

    }

}
